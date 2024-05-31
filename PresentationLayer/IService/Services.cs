using BusinessLayer.Services;
using DomainLayer.Interface.IService;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.IService
{
    /// <summary>
    /// Extension methods for configuring service dependencies.
    /// </summary>
    public static class Services
    {
        /// <summary>
        /// Adds the service dependencies to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the service dependencies will be added.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method registers all the necessary services as scoped services in the dependency injection container.
        /// </remarks>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICalanerService, CalanderService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILeaveApplyService, LeaveApplyService>();
            services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
