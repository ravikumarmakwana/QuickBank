using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountTypeAsync(AccountType accountType)
        {
            await _context.AddAsync(accountType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountTypeAsync(AccountType accountType)
        {
            _context.Update(accountType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountTypeIdAsync(long accountTypeId)
        {
            _context.Remove(await GetAccountTypeByIdAsync(accountTypeId));
            await _context.SaveChangesAsync();
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId)
        {
            return await _context.AccountTypes.FindAsync(accountTypeId);
        }
    }
}
