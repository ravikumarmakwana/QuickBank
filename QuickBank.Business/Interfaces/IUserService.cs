using QuickBank.Core.Enums;
using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegistrationRequest registrationRequest, Role role);
    }
}
