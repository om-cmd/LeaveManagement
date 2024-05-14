using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.Interface.IService
{
    public interface ILeaveTypeService
    {
        ICollection<LeaveTypeViewModel> LeaveTypeList();
        LeaveType CreateLeaveType(LeaveTypeViewModel model);
        LeaveType UpdateLeaveType(LeaveTypeViewModel model, int id);
        LeaveTypeViewModel DeleteLeaveType(int id);
    }
}
