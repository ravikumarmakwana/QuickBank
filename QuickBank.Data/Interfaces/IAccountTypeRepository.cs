using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task AddAccountTypeAsync(AccountType accountType);
        Task UpdateAccountTypeAsync(AccountType accountType);
        Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId);
        Task DeleteAccountTypeIdAsync(long accountTypeId);
    }
}
