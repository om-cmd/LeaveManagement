using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveTypeRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public LeaveType CreateLeaveType(LeaveTypeViewModel model)
        {

            var leaveType = _mapper.Map<LeaveType>(model);
            _unitOfWork.Context.LeaveType.Add(leaveType);
            _unitOfWork.Context.SaveChanges();
            return leaveType;

        }

        public LeaveTypeViewModel DeleteLeaveType(int id)
        {

            var leaveType = _unitOfWork.Context.LeaveType.FirstOrDefault(x => x.Id == id);
            if (leaveType == null)
            {
                throw new KeyNotFoundException($"Leave type with ID {id} not found.");
            }

            leaveType.IsLeave = true;
            _unitOfWork.Context.SaveChanges();

            return _mapper.Map<LeaveTypeViewModel>(leaveType);

        }

        public ICollection<LeaveTypeViewModel> LeaveTypeList()
        {

            var leaveTypes = _unitOfWork.Context.LeaveType.ToList();
            return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);

        }

        public LeaveType UpdateLeaveType(LeaveTypeViewModel model, int id)
        {

            var leaveType = _unitOfWork.Context.LeaveType.FirstOrDefault(x => x.Id == id);
            if (leaveType == null)
            {
                throw new KeyNotFoundException($"Leave type with ID {id} not found.");
            }

            _mapper.Map(model, leaveType);
            _unitOfWork.Context.SaveChanges();

            return leaveType;
        }
    }
}
