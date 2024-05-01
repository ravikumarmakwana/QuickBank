using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [Route("authenticate")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            return Ok(await _authenticationService.AuthenticateAsync(authenticationRequest));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponse>> GenerateAccessTokenAsync(TokenRequest tokenRequest)
        {
            return Ok(await _authenticationService.GetAccessTokenAsync(tokenRequest.RefreshToken));
        }
    }
}
