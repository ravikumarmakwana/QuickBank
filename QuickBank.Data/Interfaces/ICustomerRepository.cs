using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<bool> DoesAadharNumberExistsAsync(string aadharNumber);
        Task<bool> DoesPANExistsAsync(string panNumber);
        Task<Customer> GetCustomerByIdAsync(long customerId);
    }
}
