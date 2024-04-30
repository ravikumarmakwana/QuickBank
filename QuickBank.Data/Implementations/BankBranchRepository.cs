using Microsoft.EntityFrameworkCore;
using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;

namespace QuickBank.Data.Implementations
{
    public class BankBranchRepository : IBankBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BankBranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesBankBranchExistsAsync(string bankCode, string reservedCharacter, string branchCode)
        {
            return await _context.BankBranches
                .AnyAsync(_ => _.BankCode == bankCode
                    && _.ReservedCharacter == reservedCharacter
                    && _.BranchCode == branchCode
                    );
        }
    }
}
