using QuickBank.Entities;

namespace QuickBank.Business.ServiceValidators
{
    public interface IAccountServiceValidator
    {
        Task DoesIFSCExists(string IFSC);
        Task DoesAccountTypeExists(long accountTypeId);
        void ValidateAccountExists(long accountId, Account account);
    }
}
