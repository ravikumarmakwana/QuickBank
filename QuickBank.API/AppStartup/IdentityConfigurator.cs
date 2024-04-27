using Microsoft.AspNetCore.Identity;
using QuickBank.Data.Contexts;
using QuickBank.Entities;

namespace QuickBank.API.AppStartup
{
    public static class IdentityConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<long>>(option =>
                {
                    option.Password.RequireDigit = true;
                    option.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
