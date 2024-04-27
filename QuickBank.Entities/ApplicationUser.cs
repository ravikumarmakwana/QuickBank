using Microsoft.AspNetCore.Identity;

namespace QuickBank.Entities
{
    public class ApplicationUser: IdentityUser<long>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
