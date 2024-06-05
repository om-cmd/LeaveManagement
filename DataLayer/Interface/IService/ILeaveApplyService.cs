using DomainLayer.Models;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.Interface.IService
{
    public interface ILeaveApplyService
    {
        LeaveApplyViewModel GetID(int id);
        ICollection<LeaveApplyViewModel> GetLeaveApplyList();
        LeaveApply CreateLeaveApplication(LeaveApplyViewModel model);
        LeaveApply UpdateLeaveApplication(int id, LeaveApplyViewModel model);
        LeaveApplyViewModel DeleteLeaveApplication(int id);
        Task<LeaveApply> UpdateLeaveApplicationStatusAsync(int id, ApprovalStatus status);

    }
}
