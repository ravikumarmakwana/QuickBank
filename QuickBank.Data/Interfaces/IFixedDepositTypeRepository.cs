using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface IFixedDepositTypeRepository
    {
        Task AddFixedDepositTypeAsync(FixedDepositType fixedDepositType);
        Task UpdateFixedDepositTypeAsync(FixedDepositType fixedDepositType);
        Task<FixedDepositType> GetFixedDepositTypeByIdAsync(long fixedDepositTypeId);
        Task DeleteFixedDepositTypeIdAsync(long fixedDepositTypeId);
    }
}
