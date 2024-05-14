using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepo _leaveType;
        public LeaveTypeService(ILeaveTypeRepo leaveType)
        {
            _leaveType = leaveType;
        }

        public LeaveType CreateLeaveType(LeaveTypeViewModel model)
        {
            return _leaveType.CreateLeaveType(model);
        }

        public LeaveTypeViewModel DeleteLeaveType(int id)
        {
            return _leaveType.DeleteLeaveType(id);
        }

        public ICollection<LeaveTypeViewModel> LeaveTypeList()
        {
            return _leaveType.LeaveTypeList();
        }

        public LeaveType UpdateLeaveType(LeaveTypeViewModel model, int id)
        {
            return _leaveType.UpdateLeaveType(model, id);
        }
    }
}
