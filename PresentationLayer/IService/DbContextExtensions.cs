using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DomainLayer.Data;

namespace BusinessLayer.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LeaveDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
