using QuickBank.Entities;

namespace QuickBank.Business.ServiceValidators
{
    public class FixedDepositServiceValidator : IFixedDepositServiceValidator
    {
        public void ValidateFixedDepositTypeExists(long fixedDepositTypeId, FixedDepositType fixedDepositType)
        {
            if (fixedDepositType == null)
            {
                throw new InvalidOperationException(
                    $"FixedDepositType doesn't exists for given FixedDepositTypeId: {fixedDepositTypeId}"
                    );
            }
        }

        public void ValidateFixedDepositExists(long fixedDepositId, FixedDeposit fixedDeposit)
        {
            if (fixedDeposit == null)
            {
                throw new InvalidOperationException(
                    $"FixedDeposit doesn't exists for given FixedDepositId: {fixedDepositId}"
                    );
            }
        }
    }
}
