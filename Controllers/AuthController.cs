using beakiebot_server.Clients;
using beakiebot_server.Data;
using Microsoft.AspNetCore.Mvc;

namespace beakiebot_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AzureKeyVaultClient _azureClient;
        private readonly IStorage _storage;
        private readonly TwitchUserClient _twitchClient;

        public AuthController(AzureKeyVaultClient azureClient, IStorage storage, TwitchUserClient twitchClient)
        {
            _azureClient = azureClient;
            _storage = storage;
            _twitchClient = twitchClient;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string code)
        {
            await _twitchClient.UserInit(code);
            return Ok();
        }
    }
}
