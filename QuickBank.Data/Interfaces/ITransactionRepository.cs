using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionsAsync(Transaction transaction);
        Task<Transaction> GetTransactionByIdAsync(long transactionId);
        Task<List<Transaction>> GetTransactionsAsync(long accountId, DateTime startDate, DateTime endDate);
        Task<Transaction> GetLastTransactionAsync(long accountId);
    }
}
