using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// Provides methods to apply for leave and manage leave applications.
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    [ApiController]

  
    public class LeaveApplyController : ControllerBase
    {
        private readonly ILeaveApplyService _leaveApply;
        /// <summary>
        /// used to inject the service
        /// </summary>
        /// <param name="leaveApply"> this is the leaveApply service</param>
        public LeaveApplyController(ILeaveApplyService leaveApply)
        {
            _leaveApply = leaveApply;
        }

        /// <summary>
        /// Retrieves a list of all leave applications.
        /// </summary>
        /// <returns>A collection of leave application view models.</returns>

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetLeaveApplyList()
        {
            var leave = _leaveApply.GetLeaveApplyList();
            return Ok(leave);
        }

        /// <summary>
        /// Creates a new leave application.
        /// </summary>
        /// <param name="model">The leave application view model.</param>
        /// <returns>The created leave application entity.</returns>
        /// <exception cref="ArgumentException">Thrown when the employee is not found.</exception>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateLeaveApplication([FromBody] LeaveApplyViewModel model)
        {
            var createLeave = _leaveApply.CreateLeaveApplication(model);
            return Ok(createLeave);
        }

        /// <summary>
        /// Updates an existing leave application.
        /// </summary>
        /// <param name="id">The identifier of the leave application to update.</param>
        /// <param name="model">The leave application view model containing updated information.</param>
        /// <returns>The updated leave application entity, or null if not found.</returns>

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateLeaveApplication(int id, [FromBody] LeaveApplyViewModel model)
        {
            var updateLeave = _leaveApply.UpdateLeaveApplication(id, model);
            if (updateLeave == null)
            {
                return NotFound();
            }
            return Ok(updateLeave);
        }


        /// <summary>
        /// Deletes a leave application.
        /// </summary>
        /// <param name="id">The identifier of the leave application.</param>
        /// <returns>The deleted leave application view model, or null if not found.</returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLeaveApplication(int id)
        {
            var delete = _leaveApply.DeleteLeaveApplication(id);
            if (delete == null)
            {
                return NotFound();
            }
            return Ok(delete);
        }
    }
}
