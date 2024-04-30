using Microsoft.EntityFrameworkCore;
using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionsAsync(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(long transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }

        public async Task<List<Transaction>> GetTransactionsAsync(long accountId, DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Where(r => r.AccountId == accountId
                    && r.TransactionDate >= startDate
                    && r.TransactionDate <= endDate
                )
                .OrderBy(r => r.TransactionDate)
                .ToListAsync();
        }

        public async Task<Transaction> GetLastTransactionAsync(long accountId)
        {
            return await _context.Transactions
                .Where(_ => _.AccountId == accountId)
                .OrderByDescending(_ => _.TransactionDate)
                .FirstOrDefaultAsync();
        }
    }
}
