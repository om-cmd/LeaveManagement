using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var leaveType = _mapper.Map<LeaveType>(model);
                _unitOfWork.Context.LeaveType.Add(leaveType);
                _unitOfWork.Context.SaveChanges();
                return leaveType;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create leave type: {ex.Message}");
            }
        }

        public LeaveTypeViewModel DeleteLeaveType(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete leave type: {ex.Message}");
            }
        }

        public ICollection<LeaveTypeViewModel> LeaveTypeList()
        {
            try
            {
                var leaveTypes = _unitOfWork.Context.LeaveType.ToList();
                return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve leave types: {ex.Message}");
            }
        }

        public LeaveType UpdateLeaveType(LeaveTypeViewModel model, int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to update leave type: {ex.Message}");
            }
        }
    }
}
