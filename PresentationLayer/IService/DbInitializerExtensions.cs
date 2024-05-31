using Microsoft.Extensions.DependencyInjection;
using DomainLayer.DBSeeding;

namespace BusinessLayer.Extensions
{
    /// <summary>
    /// Extension methods for configuring the database initializer.
    /// </summary>
    public static class DbInitializerExtensions
    {
        /// <summary>
        /// Adds the database initializer to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the database initializer will be added.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method registers the <see cref="DbInitializer"/> as a scoped service in the dependency injection container.
        /// </remarks>
        public static IServiceCollection AddDbInitializer(this IServiceCollection services)
        {
            services.AddScoped<DbInitializer>();
            return services;
        }
    }
}
