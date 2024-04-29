using QuickBank.Data.Interfaces;
using QuickBank.Entities.Enums;
using QuickBank.Entities;
using QuickBank.Business.Exceptions;

namespace QuickBank.Business.ServiceValidators
{
    public class CustomerServiceValidator : ICustomerServiceValidator
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServiceValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void ValidateCustomerExists(long customerId, Customer customer)
        {
            if (customer == null)
            {
                throw new CustomerNotFoundException(
                    $"Customer doesn't exists for given CustomerId: {customerId}"
                );
            }

            if (customer.CustomerStatus == CustomerStatus.Inactive)
            {
                throw new InvalidOperationException(
                    $"Customer status of given CustomerId: {customerId} is {customer.CustomerStatus}"
                    );
            }
        }

        public async Task ValidateCustomerPIIAsync(string aadharNumber, string pANNumber)
        {
            bool isAadharExists = await _customerRepository.DoesAadharNumberExistsAsync(aadharNumber);
            if (isAadharExists)
            {
                throw new InvalidOperationException(
                    $"Customer with same AadharCard Number: {aadharNumber} exists."
                );
            }

            bool isPANExists = await _customerRepository.DoesPANExistsAsync(pANNumber);
            if (isPANExists)
            {
                throw new InvalidOperationException(
                    $"Customer with same PAN: {pANNumber} exists."
                );
            }
        }
    }
}
