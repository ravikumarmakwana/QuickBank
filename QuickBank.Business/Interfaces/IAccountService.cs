using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(AccountCreationRequest accountCreationRequest);
        Task CloseAccountByAccountIdAsync(long accountId);
        Task<AccountDto> GetAccountByAccountIdAsync(long accountId);
        Task<List<AccountDto>> GetAccountsByCustomerIdAsync(long customerId);
        Task DepositQuarterlyInterest(int financialQuarter);
    }
}
