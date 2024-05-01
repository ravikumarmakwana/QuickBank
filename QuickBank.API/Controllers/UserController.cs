using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Enums;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [Route("Users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdminAsync(RegistrationRequest registrationRequest)
        {
            return Ok(await RegisterAsync(registrationRequest, Role.Admin));
        }

        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            return Ok(await RegisterAsync(registrationRequest, Role.User));
        }

        private async Task<ActionResult> RegisterAsync(RegistrationRequest registrationRequest, Role role)
        {
            return Ok(await _userService.RegisterAsync(registrationRequest, role));
        }
    }
}
