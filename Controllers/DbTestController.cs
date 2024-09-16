using beakiebot_server.Data;
using Microsoft.AspNetCore.Mvc;

namespace beakiebot_server.Controllers
{
    public class DbTestController : Controller
    {
        private readonly UserContext _userContext;

        public DbTestController(UserContext context)
        {
            _userContext = context;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userContext.Users.ToList();
            return Ok(users);
        }
    }
}
