using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// Provides methods to calculate and retrieve leave balance information.
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceService _leaveBalanceService;
        /// <summary>
        /// use to call service of balance
        /// </summary>
        /// <param name="leaveBalanceService"></param>
        public LeaveBalanceController(ILeaveBalanceService leaveBalanceService)
        {
            _leaveBalanceService = leaveBalanceService;
        }

        /// <summary>
        /// Retrieves the leave balance for the current year.
        /// </summary>
        /// <returns>The leave balance for the current year.</returns>

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult LeaveBalanceList()
        {
            var balanceList = _leaveBalanceService.LeaveBalanceList();
            return Ok(balanceList);
        }

        /// <summary>
        /// Calculates the leave balance for an employee based on their position and leave usage.
        /// </summary>
        /// <param name="request">The leave calculation request containing employee ID.</param>
        /// <returns>The calculated or updated leave balance.</returns>

        [HttpPost("CalculateLeave")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CalculateLeave([FromBody] LeaveCalculationRequest request)
        {
            var calculate = _leaveBalanceService.CalculateLeave(request);
            return Ok(calculate);
        }
    }
}
