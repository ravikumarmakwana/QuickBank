using QuickBank.Data.Contexts;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;

namespace QuickBank.Data.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}
