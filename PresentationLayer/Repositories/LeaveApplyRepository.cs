using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
    public class LeaveApplyRepository : ILeaveApplyRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveApplyRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public LeaveApply CreateLeaveApplication(LeaveApplyViewModel model)
        {
            Employee employee = _unitOfWork.Context.Employee.Find(model.EmployeeID);
            if (employee == null)
            {
              
                throw new ArgumentException("Employee not found");
            }
            //model.Position = employee.Position.ToString();


            var leaveApplication = _mapper.Map<LeaveApply>(model);
            _unitOfWork.Context.LeaveApply.Add(leaveApplication);
            _unitOfWork.Context.SaveChanges();
            return leaveApplication;

        }

        public LeaveApplyViewModel DeleteLeaveApplication(int id)
        {
            var leaveApplication = _unitOfWork.Context.LeaveApply.Find(id);
            if (leaveApplication == null)
            {
                return null;
            }
            _unitOfWork.Context.LeaveApply.Remove(leaveApplication);
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<LeaveApplyViewModel>(leaveApplication);
        }

        public LeaveApplyViewModel GetID(int id)
        {
            var leaveApplication = _unitOfWork.Context.LeaveApply
               .Include(x => x.Employee)
               .Include(y => y.LeaveType)
               .FirstOrDefault(x => x.Id == id);
            return _mapper.Map<LeaveApplyViewModel>(leaveApplication);
        }

        public ICollection<LeaveApplyViewModel> GetLeaveApplyList()
        {
            var leaveApplications = _unitOfWork.Context.LeaveApply
               .Include(x => x.Employee)
               .Include(y => y.LeaveType)
               .ToList();
            return _mapper.Map<List<LeaveApplyViewModel>>(leaveApplications);
        }

        public LeaveApply UpdateLeaveApplication(int id, LeaveApplyViewModel model)
        {
            var leaveApplication = _unitOfWork.Context.LeaveApply.Find(id);
            if (leaveApplication == null)
            {
                return null;
            }
            _mapper.Map(model, leaveApplication);
            _unitOfWork.Context.SaveChanges();
            return leaveApplication;
        }
    }
}
