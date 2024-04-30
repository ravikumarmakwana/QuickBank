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
            services.AddScoped<IFixedDepositService, FixedDepositService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddServiceValidators(this IServiceCollection services)
        {
            services.AddScoped<ICustomerServiceValidator, CustomerServiceValidator>();
            services.AddScoped<IAccountServiceValidator, AccountServiceValidator>();
            services.AddScoped<IFixedDepositServiceValidator, FixedDepositServiceValidator>();
        }
    }
}
