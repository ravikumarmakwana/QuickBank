using Microsoft.EntityFrameworkCore;
using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    internal class FixedDepositRepository : IFixedDepositRepository
    {
        private readonly ApplicationDbContext _context;

        public FixedDepositRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFixedDeposit(FixedDeposit fixedDeposit)
        {
            await _context.AddAsync(fixedDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFixedDeposit(FixedDeposit fixedDeposit)
        {
            _context.Update(fixedDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task<FixedDeposit> GetFixedDepositById(long fixedDepositId)
        {
            return await _context.FixedDeposits
                .Include(s => s.FixedDepositType)
                .FirstOrDefaultAsync(r => r.FixedDepositId == fixedDepositId);
        }

        public async Task<List<FixedDeposit>> GetAllFixedDepositForAccounts(List<long> accountIds)
        {
            return await _context.FixedDeposits
                .Include(s => s.FixedDepositType)
                .Where(r => accountIds.Contains(r.FixedDepositId))
                .ToListAsync();
        }

        public async Task<List<FixedDeposit>> GetAllActiveFixedDeposits()
        {
            return await _context.FixedDeposits
                .Include(s => s.FixedDepositType)
                .Where(_ => _.IsActive)
                .ToListAsync();
        }
    }
}
