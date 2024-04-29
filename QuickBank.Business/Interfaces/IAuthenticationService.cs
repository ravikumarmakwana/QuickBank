using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<TokenResponse> GetAccessTokenAsync(string refreshToken);
    }
}
