using QuickBank.Entities.Enums;
using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface ITransactionService
    {
        Task<string> Deposit(DepositRequest depositRequest);
        Task<string> Withdrawal(WithdrawalRequest withdrawalRequest);
        Task<string> TransferFundAsync(FundTransferRequest fundTransferRequest);
        Task<List<TransactionDto>> GetTransactionsAsync(
            long accountId, TransactionRange transactionRange, DateTime? startDate, DateTime? endDate);
        Task<TransactionDto> GetTransactionByIdAsync(long transactionId);
        Task<TransactionDto> GetTransactionByReferenceNumberAsync(string referenceNumber);
        Task UpdateAccountStatus();
    }
}
