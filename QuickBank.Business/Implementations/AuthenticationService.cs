using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Helper;
using QuickBank.Core.Interfaces;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IApplicationConfiguration applicationConfiguration,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _applicationConfiguration = applicationConfiguration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            var user = await _userManager.FindByNameAsync(authenticationRequest.UserName);
            await ValidateUserCredentials(authenticationRequest, user);

            var accessToken = await GenerateAccessTokenAsync(user);
            await SaveRefreshTokenAsync(user);

            var authenticationResponse = _mapper.Map<AuthenticationResponse>(user);
            authenticationResponse.AccessToken = accessToken;

            return authenticationResponse;
        }

        public async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                throw new InvalidOperationException($"Refresh Token is Expired.");
            }

            var accessToken = await GenerateAccessTokenAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        private async Task ValidateUserCredentials(AuthenticationRequest authenticationRequest, ApplicationUser user)
        {
            if (user == null)
            {
                throw new InvalidOperationException($"User not exists for given UserName: {authenticationRequest.UserName}");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, authenticationRequest.Password);
            if (!isPasswordCorrect)
            {
                throw new InvalidOperationException("Invalid Password");
            }
        }

        private async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = TokenUtility.GetUserClaims(user, userRoles);
            var accessToken = TokenUtility.GenerateAccessToken(claims, _applicationConfiguration);
            await SaveRefreshTokenAsync(user);

            return accessToken;
        }

        private async Task SaveRefreshTokenAsync(ApplicationUser user)
        {
            user.RefreshToken = TokenUtility.GenerateRefreshToken(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_applicationConfiguration.RefreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
        }
    }
}