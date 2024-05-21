using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]


    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
       private readonly ILeaveBalanceService _leaveBalanceService;
        public LeaveBalanceController(ILeaveBalanceService leaveBalanceService) 
        {
            _leaveBalanceService = leaveBalanceService;
        }
        [HttpGet]
        public IActionResult LeaveBalanceList()
        {
           var balanceList = _leaveBalanceService.LeaveBalanceList();
            return Ok(balanceList);
        }

        [HttpPost("CalculateLeave")]
        public IActionResult CalculateLeave([FromBody] LeaveCalculationRequest request)
        {
           var calculate = _leaveBalanceService.CalculateLeave(request);    
            return Ok(calculate);
        }
    }
}
