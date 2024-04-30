using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace QuickBank.Data.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _context.Accounts.Include(s => s.Transactions).Include(s => s.FixedDeposits).ToListAsync();
        }

        public async Task<Account> GetAccountByAccountIdAsync(long accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(s => s.AccountId == accountId);
        }

        public async Task<List<Account>> GetAccountsByCustomerIdAsync(long customerId)
        {
            return await _context.Accounts.Where(s => s.CustomerId == customerId).ToListAsync();
        }
    }
}
