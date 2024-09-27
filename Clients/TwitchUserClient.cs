using beakiebot_server.Data;
using beakiebot_server.Models;
using beakiebot_server.Models.TwitchResponse;
using beakiebot_server.Utils;
using System.Text.Json;

namespace beakiebot_server.Clients
{
    public class TwitchUserClient
    {
        private readonly AzureKeyVaultClient _azureClient;
        private readonly IStorage _storage;

        public TwitchUserClient(AzureKeyVaultClient azureKeyVaultClient, IStorage storage)
        {
            _storage = storage;
            _azureClient = azureKeyVaultClient;
        }        
        
        public async Task UserInit(string code)
        {
            var tokenResponse = await TwitchGetToken(code);
            var getUserResponse = await TwitchGetUser(tokenResponse);

            var userExists = await CheckUserExists(getUserResponse.Data[0].Login);

            if (!userExists)
            {
                CreateNewUser(tokenResponse, getUserResponse.Data[0]);
            }
            else
            {
                Console.WriteLine("No need to create, user exists!");
            }
        }

        private void CreateNewUser(UserAuthTokenResponse tokenResponse, GetUserResponseData getUserResponse)
        {
            var user = new User()
            {
                Login = getUserResponse.Login,
                DisplayName = getUserResponse.DisplayName,
                ProfileImageUrl = getUserResponse.ProfileImageUrl,
                Email = getUserResponse.Email,
                AuthToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                ExpiresIn = tokenResponse.ExpiresIn,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
            };

           _storage.Add(user);
        }

        private async Task<bool> CheckUserExists(string login)
        {
            var user = await _storage.GetUser(login);
            return user != null;
        }

        private async Task<UserAuthTokenResponse> TwitchGetToken(string code)
        {
            var values = new Dictionary<string, string>
            {
                { "client_id", _azureClient.TwitchClientId! },
                { "client_secret", _azureClient.TwitchClientSecret! },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _azureClient.RedirectUrl }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await HttpClientHelper.Client().PostAsync("https://id.twitch.tv/oauth2/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<UserAuthTokenResponse>(responseString);

            return tokenResponse;
        }

        private async Task<GetUserResponse> TwitchGetUser(UserAuthTokenResponse userAuthToken)
        {
            HttpClientHelper.Client().DefaultRequestHeaders.Add("Authorization", "Bearer " + userAuthToken.AccessToken);
            HttpClientHelper.Client().DefaultRequestHeaders.Add("Client-Id", _azureClient.TwitchClientId);

            var response = await HttpClientHelper.Client().GetAsync("https://api.twitch.tv/helix/users");
            var responseString = await response.Content.ReadAsStringAsync();

            var getUserResponse = JsonSerializer.Deserialize<GetUserResponse>(responseString);

            return getUserResponse;
        }
    }
}
