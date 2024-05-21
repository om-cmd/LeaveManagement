using BusinessLayer.Repositories;
using DomainLayer.IRepoInterface.IRepo;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.RepoService
{
    public static class RepoServiceCon
    {
        public static IServiceCollection Repository(this IServiceCollection services)
        {
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
