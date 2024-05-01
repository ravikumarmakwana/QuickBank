using Microsoft.EntityFrameworkCore;
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

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesAadharNumberExistsAsync(string aadharNumber)
        {
            return await _context.Customers.AnyAsync(s => s.AadharNumber == aadharNumber);
        }

        public async Task<bool> DoesPANExistsAsync(string panNumber)
        {
            return await _context.Customers.AnyAsync(s => s.PAN == panNumber);
        }

        public async Task<Customer> GetCustomerByIdAsync(long customerId)
        {
            return await _context.Customers
                .Include(s => s.Addresses)
                .FirstOrDefaultAsync(s => s.CustomerId == customerId);
        }
    }
}
