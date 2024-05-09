using AutoMapper;
using DomainLayer.AcessLayer;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalanderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CalanderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CalanderList(DateTime startDate, DateTime endDate)
        {
            try
            {

                IQueryable<Calander> modelQuery = _unitOfWork.Context.HolidayCalanders.Where(h => h.Date >= startDate && h.Date <= endDate);
                IQueryable<CalanderViewModel> vnQuery = _mapper.ProjectTo<CalanderViewModel>(modelQuery);
                List<CalanderViewModel> list = vnQuery.ToList();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving calendar events: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetHoliday(int id)
        {
            try
            {
                var holiday = _unitOfWork.Context.HolidayCalanders.Find(id);

                if (holiday == null)
                {
                    return NotFound("Holiday not found");
                }

                var holidayViewModel = _mapper.Map<CalanderViewModel>(holiday);

                return Ok(holidayViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving holiday: {ex.Message}");
            }
        }
        [HttpPost("add")]
        public IActionResult AddHoliday([FromBody] CalanderViewModel holidayViewModel)
        {
            try
            {
                if (holidayViewModel == null)
                {
                    return BadRequest("Invalid holiday data");
                }

                // Validate holidayViewModel here if necessary

                var holiday = _mapper.Map<Calander>(holidayViewModel);
                holiday.IsPublicHoliday = true;

                _unitOfWork.Context.HolidayCalanders.Add(holiday);
                _unitOfWork.Context.SaveChanges();

                // Return the created holiday object or its ID
                return CreatedAtAction(nameof(GetHoliday), new { id = holiday.HolidayCalendarID }, holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding holiday: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveHoliday(int id)
        {
            try
            {
                // Retrieve the holiday from the database
                var holiday = _unitOfWork.Context.HolidayCalanders.Find(id);
                if (holiday == null)
                {
                    return NotFound("Holiday not found");
                }
                holiday.IsPublicHoliday = false;

                // Remove the holiday from the database
                _unitOfWork.Context.HolidayCalanders.Remove(holiday);
                _unitOfWork.Context.SaveChanges();

                return Ok("Holiday removed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing holiday: {ex.Message}");
            }
        }


    }
}
