using BusinessLayer.Repositories;
using DomainLayer.Interface.IRepo;
using DomainLayer.IRepoInterface.IRepo;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.RepoService
{
    /// <summary>
    /// Extension methods for configuring repository services.
    /// </summary>
    public static class RepoServiceCon
    {
        /// <summary>
        /// Adds the repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the repository services will be added.</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// This method registers all the necessary repositories as scoped services in the dependency injection container.
        /// </remarks>
        public static IServiceCollection Repository(this IServiceCollection services)
        {
            services.AddTransient<IEmailRepo, EmailSender>();
            services.AddScoped<ICalanderRepo, CalanderRepository>();
            services.AddScoped<IEmployeeRepo, EmployeeReposotory>();
            services.AddScoped<ILeaveApplyRepo, LeaveApplyRepository>();
            services.AddScoped<ILeaveBalanceRepo, LeaveBalanceRepository>();
            services.AddScoped<ILeaveTypeRepo, LeaveTypeRepository>();
            services.AddScoped<ILoginRepo, LoginRepository>();
            services.AddScoped<IRegisterRepo, RegisterRepository>();
            return services;
        }
    }
}
