using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DomainLayer.Data;

namespace BusinessLayer.Extensions
{
    /// <summary>
    /// Extension methods for configuring the database context.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Adds the custom database context to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the database context will be added.</param>
        /// <param name="connectionString">The connection string used to connect to the database.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method configures the <see cref="LeaveDbContext"/> to use SQL Server with the provided connection string.
        /// </remarks>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LeaveDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
