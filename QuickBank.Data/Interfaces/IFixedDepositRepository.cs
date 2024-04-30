using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface IFixedDepositRepository
    {
        Task AddFixedDeposit(FixedDeposit fixedDeposit);
        Task UpdateFixedDeposit(FixedDeposit fixedDeposit);
        Task<FixedDeposit> GetFixedDepositById(long fixedDepositId);
        Task<List<FixedDeposit>> GetAllFixedDepositForAccounts(List<long> accountIds);
        Task<List<FixedDeposit>> GetAllActiveFixedDeposits();
    }
}
