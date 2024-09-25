using beakiebot_server.Data;
using beakiebot_server.Models;
using beakiebot_server.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace beakiebot_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AzureKeyVaultClient _azureClient;
        private readonly IStorage _storage;

        public AuthController(AzureKeyVaultClient azureClient, IStorage storage)
        {
            _azureClient = azureClient;
            _storage = storage;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string code)
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

            UserAuthTokenResponse tokenResponse = JsonSerializer.Deserialize<UserAuthTokenResponse>(responseString)!;
            
            return Ok(tokenResponse);
        }
    }
}
