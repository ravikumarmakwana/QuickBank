using Microsoft.Extensions.DependencyInjection;
using QuickBank.Business.Implementations;
using QuickBank.Business.Interfaces;
using QuickBank.Business.ServiceValidators;

namespace QuickBank.Business
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }

        public static void AddServiceValidators(this IServiceCollection services)
        {
            services.AddScoped<ICustomerServiceValidator, CustomerServiceValidator>();
        }
    }
}
