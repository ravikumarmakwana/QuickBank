using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerPIIAsync(long customerId, CustomerPIIModel customerPII);
        Task UpdateEmailAsync(long customerId, UpdateEmailRequest updateEmailRequest);
        Task UpdatePhoneNumberAsync(long customerId, UpdatePhoneNumberRequest updatePhoneNumberRequest);
        Task AddAddressAsync(long customerId, AddAddressRequest addAddressRequest);
        Task UpdateAddressAsync(long customerId, long addressId, UpdateAddressRequest updateAddressRequest);
        Task DeleteAddressAsync(long customerId, long addressId);
        Task RemoveCustomerByIdAsync(long customerId);
        Task<CustomerModel> GetCustomerByIdAsync(long customerId);
    }
}
