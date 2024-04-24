using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Data.Contexts;

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
    }
}
