using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Implementations;
using QuickBank.Core.Interfaces;

namespace QuickBank.Core
{
    public static class ServiceCollectionExtension
    {
        public static void AddCoreServices(this IServiceCollection collection)
        {
            collection.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}
