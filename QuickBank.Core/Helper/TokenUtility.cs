using Microsoft.IdentityModel.Tokens;
using QuickBank.Core.Interfaces;
using QuickBank.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace QuickBank.Core.Helper
{
    public static class TokenUtility
    {
        public static List<Claim> GetUserClaims(ApplicationUser user, IList<string> userRoles)
        {
            var claims = new List<Claim>();

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.HomePhone, user.PhoneNumber));

            return claims;
        }

        public static string GenerateRefreshToken(ApplicationUser user)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static string GenerateAccessToken(List<Claim> claims, IApplicationConfiguration applicationConfiguration)
        {
            var handler = new JwtSecurityTokenHandler();

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    applicationConfiguration.Secret),
                    SecurityAlgorithms.HmacSha256
                    );

            var securityToken =
                new JwtSecurityToken(
                    applicationConfiguration.ValidIssuer,
                    applicationConfiguration.ValidAudience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(applicationConfiguration.TokenValidityInMinutes),
                    signingCredentials);

            return handler.WriteToken(securityToken);
        }
    }
}
