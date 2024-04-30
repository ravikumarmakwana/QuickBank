using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccountByAccountIdAsync(long accountId);
        Task<List<Account>> GetAccountsByCustomerIdAsync(long customerId);
    }
}
