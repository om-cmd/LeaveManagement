using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
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
