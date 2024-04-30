using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface IFixedDepositService
    {
        Task<FixedDepositDto> AddFixedDeposit(FixedDepositRequest fixedDepositRequest);
        Task CloseFixedDepositById(long fixedDepositId);
        Task<FixedDepositDto> GetFixedDepositById(long fixedDepositId);
        Task<List<FixedDepositDto>> GetAllFixedDepositForAccount(long accountId);
        Task<List<FixedDepositDto>> GetAllFixedDepositForCustomer(long customerId);
        Task CloseFixedDeposit(FixedDeposit fixedDeposit);
        Task RenewFixedDeposit(FixedDeposit fixedDeposit);
        Task ComputeFixedDeposits();
    }
}
