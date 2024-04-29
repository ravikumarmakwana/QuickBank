using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
