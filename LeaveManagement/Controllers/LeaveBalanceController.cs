using AutoMapper;
using DomainLayer.AcessLayer;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveBalanceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult LeaveBalanceList()
        {
            try
            {
                // Retrieve the leave balance including its associated employee
                IQueryable<LeaveBalance> modelQuery = _unitOfWork.Context.LeaveBalance.Include(x => x.Employee).Include(y=>y.LeaveApply);
                IQueryable<LeaveBalanceViewModel> vmQuery = _mapper.ProjectTo<LeaveBalanceViewModel>(modelQuery);
                List<LeaveBalanceViewModel> List = vmQuery.ToList();
                if (List == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Leave balance list not found");
                }

                return Ok(List);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("CalculateLeave")]
        public IActionResult CalculateLeave([FromBody] LeaveCalculationRequest request)
        {
            try
            {
                // Retrieve employee information based on the provided employee ID
                Employee employee = _unitOfWork.Context.Employee.FirstOrDefault(e => e.EmployeeID == request.EmployeeID);
                if (employee == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Employee not found");
                }

                // Get the position of the employee
                Position position = employee.Position;

                // Determine the total leave allotted based on the employee's position
                int totalLeaveAllotted = GetTotalLeaveAllotted(position);

                // Retrieve the leave applications for the employee for the given period
                var leaveApplications = _unitOfWork.Context.LeaveApply
                    .Where(la => la.EmployeeID == request.EmployeeID && la.AppliedFromDate <= request.AppliedToDate && la.AppliedToDate >= request.AppliedFromDate)
                    .ToList();

                // Calculate total leave days applied for all leave applications
                int totalLeaveDaysApplied = leaveApplications.Sum(la => (int)(la.AppliedToDate - la.AppliedFromDate).TotalDays + 1);

                // Retrieve the leave balance for the current year
                LeaveBalance leaveBalance = _unitOfWork.Context.LeaveBalance.FirstOrDefault(lb => lb.EmployeeID == request.EmployeeID && lb.Year == DateTime.Now.Year);
                if (leaveBalance == null)
                {
                    // If no leave balance exists for the current year, create a new one
                    leaveBalance = new LeaveBalance
                    {
                        EmployeeID = request.EmployeeID,
                        RemainingLeave = totalLeaveAllotted,
                        LastUpdated = DateTime.Now,
                        Year = DateTime.Now.Year,
                        AllocatedThisYear = totalLeaveAllotted,
                        UsedThisYear = 0
                    };
                    _unitOfWork.Context.LeaveBalance.Add(leaveBalance);
                }
                else
                {
                    // If leave balance exists, update the remaining leave
                    leaveBalance.RemainingLeave = totalLeaveAllotted - totalLeaveDaysApplied;
                    leaveBalance.LastUpdated = DateTime.Now;
                    _unitOfWork.Context.LeaveBalance.Update(leaveBalance);
                }

                // Map the leave balance to the view model
                LeaveBalanceViewModel viewModel = new LeaveBalanceViewModel
                {
                    EmployeeID = leaveBalance.EmployeeID,
                    RemainingLeave = leaveBalance.RemainingLeave,
                    LeaveDaysApplied = totalLeaveDaysApplied, // Assign totalLeaveDaysApplied to LeaveDaysApplied property
                    LastUpdated = leaveBalance.LastUpdated,
                    Year = leaveBalance.Year,
                    AllocatedThisYear = leaveBalance.AllocatedThisYear,
                    UsedThisYear = leaveBalance.UsedThisYear
                };

                _unitOfWork.Context.SaveChanges();

                return Ok(viewModel); // Return the view model containing the leave balance details
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        private int GetTotalLeaveAllotted(Position position)
        {
            // Determine the total leave allotted based on the position
            switch (position)
            {
                case Position.Intern:
                    return 10; // For example, interns get 10 days of leave
                case Position.Trainee:
                    return 15; // For example, trainees get 15 days of leave
                case Position.Junior:
                    return 20; // For example, juniors get 20 days of leave
                case Position.Senior:
                    return 25; // For example, seniors get 25 days of leave
                default:
                    throw new ArgumentException("Invalid position");
            }
        }
    }
}
