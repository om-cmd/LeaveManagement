using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// used for Leavy type Curd Operations
    /// </summary>
    public class LeaveTypeRepository : ILeaveTypeRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations</param>
        /// <param name="mapper">The mapper for object-object mapping</param>

        public LeaveTypeRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// adds new leavetype
        /// </summary>
        /// <param name="model"> the viewmodel parametersS </param>
        /// <returns>returns updated leave type </returns>
        public LeaveType CreateLeaveType(LeaveTypeViewModel model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _unitOfWork.Context.LeaveType.Add(leaveType);
            _unitOfWork.Context.SaveChanges();
            return leaveType;
        }

      /// <summary>
      /// deletes the leaveType
      /// </summary>
      /// <param name="id">ID used for deleted for the selected type</param>
      /// <returns>returns new updated model </returns>
      /// <exception cref="KeyNotFoundException"></exception>
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
        /// <summary>
        /// This provide lisr
        /// </summary>
        /// <returns>returns updated list</returns>
       
        public ICollection<LeaveTypeViewModel> LeaveTypeList()
        {
            var leaveTypes = _unitOfWork.Context.LeaveType.ToList();
            return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
        }

    /// <summary>
    /// updates the LeaveType
    /// </summary>
    /// <param name="model">it is the view model params</param>
    /// <param name="id">it takes id for update </param>
    /// <returns>returns update informations</returns>
    /// <exception cref="KeyNotFoundException"></exception>
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
