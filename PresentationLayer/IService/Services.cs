using BusinessLayer.Services;
using DomainLayer.Interface.IService;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.IService
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILeaveApplyService, LeaveApplyService>();
            services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            return services;

        }
    }
}
