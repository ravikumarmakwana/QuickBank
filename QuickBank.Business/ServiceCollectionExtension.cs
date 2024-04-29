using Microsoft.Extensions.DependencyInjection;
using QuickBank.Business.Implementations;
using QuickBank.Business.Interfaces;

namespace QuickBank.Business
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
