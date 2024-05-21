using Microsoft.Extensions.DependencyInjection;
using DomainLayer.DBSeeding;

namespace BusinessLayer.Extensions
{
    public static class DbInitializerExtensions
    {
        public static IServiceCollection AddDbInitializer(this IServiceCollection services)
        {
            services.AddScoped<DbInitializer>();
            return services;
        }
    }
}
