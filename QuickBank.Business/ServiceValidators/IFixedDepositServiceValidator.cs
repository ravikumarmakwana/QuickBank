using QuickBank.Entities;

namespace QuickBank.Business.ServiceValidators
{
    public interface IFixedDepositServiceValidator
    {
        void ValidateFixedDepositTypeExists(long fixedDepositTypeId, FixedDepositType fixedDepositType);
        void ValidateFixedDepositExists(long fixedDepositId, FixedDeposit fixedDeposit);
    }
}
