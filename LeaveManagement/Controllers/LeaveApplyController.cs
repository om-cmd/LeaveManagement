using AutoMapper;
using DomainLayer.AcessLayer;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveApplyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveApplyList()
        {
            try
            {
                IQueryable<LeaveApply> leaveApplicationsQuery = _unitOfWork.Context.LeaveApply
                    .Include(x => x.Employee)
                    .Include(y => y.LeaveType);

                IQueryable<LeaveApplyViewModel> leaveApplicationsViewModelQuery = _mapper.ProjectTo<LeaveApplyViewModel>(leaveApplicationsQuery);

                List<LeaveApplyViewModel> leaveApplications = await leaveApplicationsViewModelQuery.ToListAsync();

                return Ok(leaveApplications);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveApplyID(int id)
        {
            try
            {
                LeaveApply leaveApplication = _unitOfWork.Context.LeaveApply
                    .Include(x => x.Employee)
                    .Include(y => y.LeaveType)
                    .FirstOrDefault(x => x.LeaveApplyID == id);

                if (leaveApplication == null)
                {
                    return NotFound("Leave application not found");
                }

                LeaveApplyViewModel leaveApplicationViewModel = _mapper.Map<LeaveApplyViewModel>(leaveApplication);

                return Ok(leaveApplicationViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateLeaveApplication([FromBody] LeaveApplyViewModel model)
        {
            try
            {
                LeaveApply leaveApplication = _mapper.Map<LeaveApply>(model);


                _unitOfWork.Context.LeaveApply.Add(leaveApplication);
                _unitOfWork.Context.SaveChanges();


                LeaveApplyViewModel createdApplicationViewModel = _mapper.Map<LeaveApplyViewModel>(leaveApplication);

                return Ok(createdApplicationViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveApplication(int id, [FromBody] LeaveApplyViewModel model)
        {
            try
            {
                LeaveApply leaveApplication = _unitOfWork.Context.LeaveApply
                    .Include(x => x.Employee)
                    .Include(y => y.LeaveType)
                    .FirstOrDefault(x => x.LeaveApplyID == id);

                if (leaveApplication == null)
                {
                    return NotFound("Leave application not found");
                }

                _mapper.Map(model, leaveApplication);

                _unitOfWork.Context.SaveChanges();

                LeaveApplyViewModel updatedApplicationViewModel = _mapper.Map<LeaveApplyViewModel>(leaveApplication);

                return Ok(updatedApplicationViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveApplication(int id)
        {
            try
            {
                LeaveApply leaveApplication = _unitOfWork.Context.LeaveApply
                    .Include(x => x.Employee)
                    .Include(y => y.LeaveType)
                    .FirstOrDefault(x => x.LeaveApplyID == id);

                if (leaveApplication == null)
                {
                    return NotFound("Leave application not found");
                }

                _unitOfWork.Context.LeaveApply.Remove(leaveApplication);
                _unitOfWork.Context.SaveChanges();

                return Ok("Leave application deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
