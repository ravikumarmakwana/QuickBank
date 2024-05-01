using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity.Data;
using QuickBank.API.Filters;
using QuickBank.API.Validators;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.API
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBaseDependencies(this IServiceCollection services, Action<FluentValidationMvcConfiguration> fluentValidationAction = null)
        {
            IMvcBuilder mvcBuilder = services.AddControllers();
            if (fluentValidationAction != null)
            {
                mvcBuilder.AddFluentValidation(fluentValidationAction);
            }

            services.AddCors(delegate (CorsOptions options)
            {
                options.AddPolicy("CorsPolicy", delegate (CorsPolicyBuilder builder)
                {
                    builder.SetIsOriginAllowed((string host) => true).AllowAnyOrigin().AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AccountCreationRequest>, AccountCreationRequestValidator>();
            services.AddScoped<IValidator<AddAddressRequest>, AddAddressRequestValidator>();
            services.AddScoped<IValidator<CustomerPIIModel>, CustomerPIIModelValidator>();
            services.AddScoped<IValidator<DepositRequest>, DepositRequestValidator>();
            services.AddScoped<IValidator<FixedDepositRequest>, FixedDepositRequestValidator>();
            services.AddScoped<IValidator<FundTransferRequest>, FundTransferRequestValidator>();
            services.AddScoped<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
            services.AddScoped<IValidator<UpdateAddressRequest>, UpdateAddressRequestValidator>();
            services.AddScoped<IValidator<UpdateEmailRequest>, UpdateEmailRequestValidator>();
            services.AddScoped<IValidator<UpdatePhoneNumberRequest>, UpdatePhoneNumberRequestValidator>();
            services.AddScoped<IValidator<WithdrawalRequest>, WithdrawalRequestValidator>();
        }

        public static void RegisterFilters(this IServiceCollection services)
        {
            services.AddScoped<ExceptionFilter>();
        }
    }
}
