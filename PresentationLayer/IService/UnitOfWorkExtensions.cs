using Microsoft.Extensions.DependencyInjection;
using DomainLayer.UnitOfWork;
using DomainLayer.AcessLayer;

namespace BusinessLayer.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
