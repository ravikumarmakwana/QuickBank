using Microsoft.EntityFrameworkCore;
using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.RefreshTokenExpiryTime >= DateTime.Now);
        }
    }
}
