using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
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
        private readonly INotificationService _notificationService;
        private readonly ILeaveBalanceRepo _leaveBalanceRepo;

        public LeaveApplyRepository(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, ILeaveBalanceRepo leaveBalanceRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
            _leaveBalanceRepo = leaveBalanceRepo;
        }

     
        public LeaveApply CreateLeaveApplication(LeaveApplyViewModel model)
        {
            Employee employee = _unitOfWork.Context.Employee.Find(model.EmployeeID);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            var leaveApplication = _mapper.Map<LeaveApply>(model);
            leaveApplication.Status = ApprovalStatus.Pending;
            _unitOfWork.Context.LeaveApply.Add(leaveApplication);
            _unitOfWork.Context.SaveChanges();

            // Invoke leave balance calculation
            _leaveBalanceRepo.CalculateLeave(new LeaveCalculationRequest { EmployeeID = model.EmployeeID });

            return leaveApplication;
        }

        /// <summary>
        /// Updates the status of a leave application asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the leave application.</param>
        /// <param name="status">The new approval status.</param>
        /// <returns>The updated leave application entity.</returns>
        /// <exception cref="ArgumentException">Thrown when the leave application is not found.</exception>
        public async Task<LeaveApply> UpdateLeaveApplicationStatusAsync(int id, ApprovalStatus status)
        {
            var leaveApplication = await _unitOfWork.Context.LeaveApply.FindAsync(id);
            if (leaveApplication == null)
            {
                throw new ArgumentException("Leave application not found");
            }

            leaveApplication.Status = status;
            await _unitOfWork.Context.SaveChangesAsync();

            string message = status == ApprovalStatus.Approved ? "Your leave request has been approved." : "Your leave request has been rejected.";
            await _notificationService.AddNotification(leaveApplication.EmployeeId, message);

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

        /// <summary>
        /// Gets the details of a specific leave application by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the leave application.</param>
        /// <returns>The leave application view model.</returns>
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
