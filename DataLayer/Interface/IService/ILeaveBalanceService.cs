using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.Interface.IService
{
    public interface ILeaveBalanceService
    {
        LeaveBalance LeaveBalanceList();
        LeaveBalance CalculateLeave(LeaveCalculationRequest request);
    }
}
