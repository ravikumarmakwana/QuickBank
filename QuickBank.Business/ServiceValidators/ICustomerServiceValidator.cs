using QuickBank.Entities;

namespace QuickBank.Business.ServiceValidators
{
    public interface ICustomerServiceValidator
    {
        Task ValidateCustomerPIIAsync(string aadharNumber, string pANNumber);
        void ValidateCustomerExists(long customerId, Customer customer);
    }
}
