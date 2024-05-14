using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Services
{
    public class LeaveApplyService : ILeaveApplyService
    {
        private readonly ILeaveApplyRepo _apply;
        public LeaveApplyService(ILeaveApplyRepo apply)
        {
            _apply = apply;
        }

        public LeaveApply CreateLeaveApplication(LeaveApplyViewModel model)
        {
            return _apply.CreateLeaveApplication(model);
        }

        public LeaveApplyViewModel DeleteLeaveApplication(int id)
        {
            return _apply.DeleteLeaveApplication(id);
        }

        public LeaveApplyViewModel GetID(int id)
        {
            return _apply.GetID(id);
        }

        public ICollection<LeaveApplyViewModel> GetLeaveApplyList()
        {
            return _apply.GetLeaveApplyList();
        }

        public LeaveApply UpdateLeaveApplication(int id, LeaveApplyViewModel model)
        {
            return _apply.UpdateLeaveApplication(id, model);
        }
    }
}
