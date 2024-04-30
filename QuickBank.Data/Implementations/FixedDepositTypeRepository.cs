using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    internal class FixedDepositTypeRepository : IFixedDepositTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public FixedDepositTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFixedDepositTypeAsync(FixedDepositType fixedDepositType)
        {
            await _context.AddAsync(fixedDepositType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFixedDepositTypeAsync(FixedDepositType fixedDepositType)
        {
            _context.Update(fixedDepositType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFixedDepositTypeIdAsync(long fixedDepositTypeId)
        {
            _context.Remove(await GetFixedDepositTypeByIdAsync(fixedDepositTypeId));
            await _context.SaveChangesAsync();
        }

        public async Task<FixedDepositType> GetFixedDepositTypeByIdAsync(long fixedDepositTypeId)
        {
            return await _context.FixedDepositTypes.FindAsync(fixedDepositTypeId);
        }
    }
}
