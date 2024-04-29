using QuickBank.Entities;

namespace QuickBank.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
    }
}
