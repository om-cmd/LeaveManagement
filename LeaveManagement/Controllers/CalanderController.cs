using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;
using System;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// API controller for managing holiday calendar entries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalanderController : ControllerBase
    {
        private readonly ICalanerService _calanerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalanderController"/> class.
        /// </summary>
        /// <param name="calanerService">The calendar service.</param>
        public CalanderController(ICalanerService calanerService)
        {
            _calanerService = calanerService;
        }

        /// <summary>
        /// Retrieves a list of public holidays from the calendar.
        /// </summary>
        /// <returns>A list of public holidays.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CalanderList()
        {
            var list = _calanerService.CalanderList();
            return Ok(list);
        }

        /// <summary>
        /// Retrieves details of a specific holiday by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the holiday.</param>
        /// <returns>The holiday details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetHoliday(int id)
        {
            try
            {
                var holiday = _calanerService.GetHoliday(id);
                if (holiday == null)
                {
                    return NotFound();
                }
                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving holiday: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new holiday to the calendar.
        /// </summary>
        /// <param name="holidayViewModel">The holiday view model to add.</param>
        /// <returns>The added holiday.</returns>
        [HttpPost("add")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddHoliday([FromBody] CalanderViewModel holidayViewModel)
        {
            try
            {
                var holiday = _calanerService.AddHoliday(holidayViewModel);
                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding holiday: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a holiday from the calendar.
        /// </summary>
        /// <param name="id">The identifier of the holiday to remove.</param>
        /// <returns>The removed holiday.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveHoliday(int id)
        {
            try
            {
                var holiday = _calanerService.RemoveHoliday(id);
                if (holiday == null)
                {
                    return NotFound();
                }
                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing holiday: {ex.Message}");
            }
        }
    }
}
