using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ILeaveBalanceRepo
    {
        LeaveBalance LeaveBalanceList();
        LeaveBalance CalculateLeave(LeaveCalculationRequest request);
    }
}
