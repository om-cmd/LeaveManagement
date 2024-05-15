using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
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
        private readonly ICalanerService _calanerService;

        public CalanderController(ICalanerService calanerService)
        {
            _calanerService = calanerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CalanderList()
        {
           var list = _calanerService.CalanderList();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetHoliday(int id)
        {
            try
            {
                var holiday = _calanerService.GetHoliday(id);             
                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving holiday: {ex.Message}");
            }
        }
        [HttpPost("add")]
        public IActionResult AddHoliday([FromBody] CalanderViewModel holidayViewModel)
        {
            var holiday = _calanerService.AddHoliday(holidayViewModel);
            return Ok(holiday); 
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveHoliday(int id)
        {
           var holiday = _calanerService.RemoveHoliday(id);
            return Ok(holiday);
        }


    }
}
