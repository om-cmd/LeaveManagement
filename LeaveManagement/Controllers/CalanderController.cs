using AutoMapper;
using DomainLayer.AcessLayer;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
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
        [HttpGet("{id}")]
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
        [HttpPost("add")]
        public IActionResult AddHoliday([FromBody] CalanderViewModel holidayViewModel)
        {
            try
            {
                // Map view model to domain model
                var holiday = _mapper.Map<Calander>(holidayViewModel);

                holiday.IsPublicHoliday= true;

                // Add the holiday to the database
                _unitOfWork.Context.HolidayCalanders.Add(holiday);
                _unitOfWork.Context.SaveChanges();

                return Ok("Holiday added successfully");
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
                holiday.IsPublicHoliday= false;

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
