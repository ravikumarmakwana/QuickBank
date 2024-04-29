using AutoMapper;
using QuickBank.Business.Exceptions;
using QuickBank.Business.Interfaces;
using QuickBank.Business.ServiceValidators;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerServiceValidator _customerServiceValidator;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            ICustomerServiceValidator customerServiceValidator,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _customerServiceValidator = customerServiceValidator;
            _mapper = mapper;
        }

        public async Task AddAddressAsync(long customerId, AddAddressRequest addAddressRequest)
        {
            Customer customer = await GetCustomerAsync(customerId);
            if (customer.Addresses == null)
            {
                customer.Addresses = new();
            }
            customer.Addresses.Add(_mapper.Map<Address>(addAddressRequest));
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAddressAsync(long customerId, long addressId)
        {
            Customer customer = await GetCustomerAsync(customerId);
            Address? address = customer.Addresses?.FirstOrDefault(s => s.AddressId == addressId) ?? null;
            if (address == null)
            {
                throw new AddressNotFoundException(
                    $"Address doesn't exists for give AddressId: {addressId} for the Customer with CustomerId: {customerId}."
                    );
            }

            customer.Addresses?.Remove(address);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task UpdateAddressAsync(long customerId, long addressId, UpdateAddressRequest updateAddressRequest)
        {
            Customer customer = await GetCustomerAsync(customerId);
            Address? address = customer.Addresses?.FirstOrDefault(s => s.AddressId == addressId) ?? null;
            if (address == null)
            {
                throw new AddressNotFoundException(
                    $"Address doesn't exists for give AddressId: {addressId} for the Customer with CustomerId: {customerId}."
                    );
            }
            _mapper.Map<UpdateAddressRequest, Address>(updateAddressRequest, address);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task AddCustomerPIIAsync(long customerId, CustomerPIIModel customerPII)
        {
            Customer customer = await GetCustomerAsync(customerId);
            await _customerServiceValidator.ValidateCustomerPIIAsync(customerPII.AadharNumber, customerPII.PAN);
            customer.AadharNumber = customerPII.AadharNumber;
            customer.PAN = customerPII.PAN;
            customer.CustomerStatus = Entities.Enums.CustomerStatus.Active;
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task UpdateEmailAsync(long customerId, UpdateEmailRequest updateEmailRequest)
        {
            Customer customer = await GetCustomerAsync(customerId);
            customer.EmailAddress = updateEmailRequest.EmailAddress;
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task UpdatePhoneNumberAsync(long customerId, UpdatePhoneNumberRequest updatePhoneNumberRequest)
        {
            Customer customer = await GetCustomerAsync(customerId);
            customer.PhoneNumber = updatePhoneNumberRequest.PhoneNumber;
            await _customerRepository.UpdateAsync(customer);
        }

        private async Task<Customer> GetCustomerAsync(long customerId)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            _customerServiceValidator.ValidateCustomerExists(customerId, customer);
            return customer;
        }

        public async Task RemoveCustomerByIdAsync(long customerId)
        {
            Customer customer = await GetCustomerAsync(customerId);
            customer.CustomerStatus = Entities.Enums.CustomerStatus.Inactive;
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(long customerId)
        {
            return _mapper.Map<CustomerModel>(await GetCustomerAsync(customerId));
        }
    }
}
