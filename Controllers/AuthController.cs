using Microsoft.AspNetCore.Mvc;

namespace beakiebot_server.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Success");
        }
    }
}
