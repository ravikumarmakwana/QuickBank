using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Data.Contexts;
using QuickBank.Data.Implementations;
using QuickBank.Data.Interfaces;

namespace QuickBank.Data
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("Default"),
                    s => s.MigrationsAssembly("QuickBank.DbMigration")
                    )
            );
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
