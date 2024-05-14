using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly ILeaveBalanceRepo _balance;
        public LeaveBalanceService(ILeaveBalanceRepo balance)
        {
            _balance = balance;
        }
        public LeaveBalance CalculateLeave(LeaveCalculationRequest request)
        {
            return _balance.CalculateLeave(request);
        }

        public LeaveBalance LeaveBalanceList()
        {
            return _balance.LeaveBalanceList();
        }
    }
}
