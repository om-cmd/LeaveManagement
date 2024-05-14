using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ILeaveTypeRepo
    {
        ICollection<LeaveTypeViewModel> LeaveTypeList();
        LeaveType CreateLeaveType(LeaveTypeViewModel model);
        LeaveType UpdateLeaveType(LeaveTypeViewModel model, int id);
        LeaveTypeViewModel DeleteLeaveType(int id);
    }
}
