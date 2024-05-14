using DomainLayer.Models;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ILeaveApplyRepo
    {
        LeaveApplyViewModel GetID(int id);
        ICollection<LeaveApplyViewModel> GetLeaveApplyList();
        LeaveApply CreateLeaveApplication(LeaveApplyViewModel model);
        LeaveApply UpdateLeaveApplication(int id, LeaveApplyViewModel model);
        LeaveApplyViewModel DeleteLeaveApplication(int id);
    }
}
