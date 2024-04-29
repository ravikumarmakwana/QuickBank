using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickBank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController: ControllerBase
    {
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminOnly()
        {
            return Ok("Accessible");
        }

        [HttpGet("user-only")]
        [Authorize(Roles = "User")]
        public ActionResult UserOnly()
        {
            return Ok("Accessible");
        }

        [HttpGet("user-admin")]
        [Authorize(Roles = "User, Admin")]
        public ActionResult UserAndAdminOnly()
        {
            return Ok("Accessible");
        }
    }
}
