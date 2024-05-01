using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Enums;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, ICustomerRepository customerRepository, IMapper mapper)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<string> RegisterAsync(RegistrationRequest registrationRequest, Role role)
        {
            ApplicationUser? existingUser = await _userManager.FindByNameAsync(registrationRequest.Username);

            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with given Username: {registrationRequest.Username} Sis already exists.");
            }

            ApplicationUser newUser = _mapper.Map<ApplicationUser>(registrationRequest);
            IdentityResult result = await _userManager.CreateAsync(newUser, registrationRequest.Password);

            if (!result.Succeeded)
            {
                return $"Registration Failed!, {string.Join(", ", result.Errors.Select(s => s.Description))}";
            }

            await _userManager.AddToRoleAsync(newUser, role.ToString());
            Customer customer = await CreateCustomerAsync(registrationRequest, newUser);
            return $"Registration Successful, Your customerId is {customer.CustomerId}.";
        }

        private async Task<Customer> CreateCustomerAsync(RegistrationRequest registrationRequest, ApplicationUser newUser)
        {
            Customer customer = _mapper.Map<Customer>(registrationRequest);

            customer.UserId = newUser.Id;
            customer.CustomerStatus = Entities.Enums.CustomerStatus.Pending;
            customer.AadharNumber = DateTimeOffset.Now.Ticks.ToString();
            customer.PAN = DateTimeOffset.Now.Ticks.ToString();

            return await _customerRepository.CreateAsync(customer);
        }
    }
}
