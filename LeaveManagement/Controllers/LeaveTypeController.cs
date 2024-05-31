using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// Provides methods to manage leave types.
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveType;
        /// <summary>
        /// use to call leaveType Service
        /// </summary>
        /// <param name="leaveType"></param>
        public LeaveTypeController(ILeaveTypeService leaveType)
        {
            _leaveType = leaveType;
        }

        /// <summary>
        /// Retrieves a list of all leave types.
        /// </summary>
        /// <returns>A collection of leave type view models.</returns>

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult LeaveTypeList()
        {
            var typeList = _leaveType.LeaveTypeList();
            return Ok(typeList);
        }


        /// <summary>
        /// Creates a new leave type.
        /// </summary>
        /// <param name="model">The leave type view model.</param>
        /// <returns>The created leave type entity.</returns>

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateLeaveType(LeaveTypeViewModel model)
        {
            var typeCreate = _leaveType.CreateLeaveType(model);
            return Ok(typeCreate);
        }
        /// <summary>
        /// Updates an existing leave type.
        /// </summary>
        /// <param name="model">The leave type view model containing updated information.</param>
        /// <param name="id">The identifier of the leave type to update.</param>
        /// <returns>The updated leave type entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the leave type with the specified ID is not found.</exception>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypeViewModel model, int id)
        {
            var updateType = _leaveType.UpdateLeaveType(model, id);
            return Ok(updateType);
        }

        /// <summary>
        /// Deletes a leave type by marking it as leave.
        /// </summary>
        /// <param name="id">The identifier of the leave type to delete.</param>
        /// <returns>The deleted leave type view model.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the leave type with the specified ID is not found.</exception>

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteLeaveType(int id)
        {
            var deleteType = _leaveType.DeleteLeaveType(id);
            return Ok(deleteType);
        }
    }
}
