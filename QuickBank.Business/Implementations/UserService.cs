using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Enums;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
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

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, role.ToString());
                return "Registration Successful.";
            }

            return "Registration Failed.";
        }
    }
}
