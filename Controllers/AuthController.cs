using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
        private readonly Uri _keyVaultUri;
        private readonly string _twitchClientId;
        private readonly string _twitchClientSecret;
        private readonly string _redirectUrl = "https://localhost:7176/auth/login"; //Replace with azure keyvault value for prod


        private SecretClient _secretClient;

        public AuthController(IConfiguration configuration)
        {
            _keyVaultUri = new(configuration["KeyVault:KeyVaultUrl"]!);

            _secretClient = new(_keyVaultUri, new DefaultAzureCredential());

            var response = _secretClient.GetSecret("TwitchClientId");
            _twitchClientId = response.Value.Value.ToString();

            response = _secretClient.GetSecret("TwitchClientSecret");
            _twitchClientSecret = response.Value.Value.ToString();
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string code)
        {
            var values = new Dictionary<string, string>
            {
                { "client_id", _twitchClientId },
                { "client_secret", _twitchClientSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _redirectUrl }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await HttpClientHelper.Client().PostAsync("https://id.twitch.tv/oauth2/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            UserAuthTokenResponse tokenResponse = JsonSerializer.Deserialize<UserAuthTokenResponse>(responseString)!;
            
            return Ok(tokenResponse);
        }
    }
}
