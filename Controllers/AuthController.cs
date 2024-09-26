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

        public AuthController(AzureKeyVaultClient azureClient, IStorage storage)
        {
            _azureClient = azureClient;
            _storage = storage;
        }

        [HttpGet("Login")]
        public IActionResult Login(string code)
        {
            var twitchLoginClient = new TwitchUserClient(code, _azureClient, _storage);
            twitchLoginClient.UserInit();
            return Ok();
        }
    }
}
