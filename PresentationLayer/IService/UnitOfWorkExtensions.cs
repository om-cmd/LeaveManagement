using Microsoft.Extensions.DependencyInjection;
using DomainLayer.UnitOfWork;
using DomainLayer.AcessLayer;

namespace BusinessLayer.Extensions
{
    /// <summary>
    /// Extension methods for configuring Unit of Work dependencies.
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// Adds the Unit of Work dependency to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the Unit of Work dependency will be added.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method registers the Unit of Work as a scoped service in the dependency injection container.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
