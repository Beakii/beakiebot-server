using beakiebot_server.Data;
using Microsoft.AspNetCore.Mvc;

namespace beakiebot_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DbTestController(IStorage storage) : Controller
    {

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(storage.GetAllUsers());
        }
    }
}
