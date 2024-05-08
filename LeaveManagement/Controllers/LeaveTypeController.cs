using AutoMapper;
using DomainLayer.AcessLayer;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LeaveTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> LeaveTypeList()
        {
            try
            {
                IQueryable<LeaveType> modelQuery = _unitOfWork.Context.LeaveType;
                IQueryable<LeaveTypeViewModel> vmQyery = _mapper.ProjectTo<LeaveTypeViewModel>(modelQuery);
                List<LeaveTypeViewModel> list = await vmQyery.ToListAsync();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $" error has occured in server side: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult LeaveId(int id)
        {
            try
            {
                var employee = _unitOfWork.Context.LeaveType.Find(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error has occured in server side:{ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult CreateLeaveType(LeaveTypeViewModel model)
        {
            try
            {
                var create = _mapper.Map<LeaveType>(model);
                _unitOfWork.Context.Add(create);
                _unitOfWork.Context.SaveChanges();
                return Ok(create);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"there is error in server side:{e.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypeViewModel model, int id)
        {
            try
            {
                var update = _unitOfWork.Context.LeaveType.FirstOrDefault(x => x.LeaveTypeID == id);
                if (update == null)
                {
                    return NotFound($"Leave type with ID {id} not found.");
                }
                _mapper.Map(model, update);
                await _unitOfWork.Context.SaveChangesAsync();
                return Ok(update);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"server error:{e.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveType(int id)
        {
            try
            {
                var delete = _unitOfWork.Context.LeaveType.FirstOrDefault(x => x.LeaveTypeID == id);
                if(delete==null)
                {
                    return NotFound($"type id {id} not found.");
                }
                delete.IsLeave = true;
                _unitOfWork.Context.SaveChanges();
                return Ok(delete);  
            }
            catch(Exception e)
            {
                return StatusCode(500,$"server error :{e.Message}");
            }
        }

    }
}
