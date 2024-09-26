using beakiebot_server.Data;
using beakiebot_server.Models.TwitchResponse;
using beakiebot_server.Utils;
using System.Text.Json;

namespace beakiebot_server.Clients
{
    public class TwitchUserClient
    {
        private readonly string _code;
        private readonly AzureKeyVaultClient _azureClient;
        private readonly IStorage _storage;

        public TwitchUserClient(string code, AzureKeyVaultClient azureKeyVaultClient, IStorage storage)
        {
            _storage = storage;
            _azureClient = azureKeyVaultClient;
            _code = code;
        }
        
        public async void UserInit()
        {
            var tokenResponse = await TwitchGetToken();
            var getUserResponse = await TwitchGetUser(tokenResponse);

            var userExists = CheckUserExists(getUserResponse.Data[0].DisplayName);

            if (!userExists)
            {
                CreateNewUser();
            }
            
        }

        public async void CreateNewUser()
        {
            //TODO
            //Create new user object
            //Add to DB
        }

        public bool CheckUserExists(string displayName)
        {
            //TODO
            //Search DB for displayname
            //If already in DB return true
            //else return false
            return false;
        }

        private async Task<UserAuthTokenResponse> TwitchGetToken()
        {
            var values = new Dictionary<string, string>
            {
                { "client_id", _azureClient.TwitchClientId! },
                { "client_secret", _azureClient.TwitchClientSecret! },
                { "code", _code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _azureClient.RedirectUrl }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await HttpClientHelper.Client().PostAsync("https://id.twitch.tv/oauth2/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<UserAuthTokenResponse>(responseString);

            return tokenResponse;
        }

        public async Task<GetUserResponse> TwitchGetUser(UserAuthTokenResponse userAuthToken)
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
