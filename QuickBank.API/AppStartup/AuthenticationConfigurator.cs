using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuickBank.Core.Interfaces;

namespace QuickBank.API.AppStartup
{
    public static class AuthenticationConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            var applicationConfiguration = services.BuildServiceProvider()
                .GetService<IApplicationConfiguration>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = applicationConfiguration?.ValidAudience,
                        ValidIssuer = applicationConfiguration?.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(applicationConfiguration?.Secret),
                        RequireExpirationTime = true
                    };
                });
        }
    }
}
