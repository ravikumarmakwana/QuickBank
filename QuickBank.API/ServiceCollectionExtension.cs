using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;

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
    }
}
