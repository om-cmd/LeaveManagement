using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

public class LeaveBalanceRepository : ILeaveBalanceRepo
{
    private readonly IUnitOfWork _unitOfWork;

    public LeaveBalanceRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public LeaveBalance CalculateLeave(LeaveCalculationRequest request)
    {
        // Retrieve employee information based on the provided employee ID
        var employee = _unitOfWork.Context.Employee.FirstOrDefault(e => e.Id == request.EmployeeID);
        if (employee == null)
        {
            throw new ArgumentException("Employee not found");
        }

        // Ensure LeaveType exists
        var leaveType = _unitOfWork.Context.LeaveType.FirstOrDefault(e => e.Id == request.LeaveTypeId);
        if (leaveType == null)
        {
            throw new ArgumentException("Leave type not found");
        }

        // Get the position of the employee
        Position position = employee.Position;

        // Determine the total leave allotted based on the employee's position
        int totalLeaveAllotted = GetTotalLeaveAllotted(position);

        // Ensure LeaveApply exists for the current leave request
        LeaveApply leaveApply = new LeaveApply
        {
            LeaveApplyEnabled = true,
            LeaveApplyDescription = "Auto-generated leave application",
            AppliedFromDate = request.AppliedFromDate,
            AppliedToDate = request.AppliedToDate,
            Status = ApprovalStatus.Pending,
            EmployeeId = request.EmployeeID,
            LeaveTypeId = request.LeaveTypeId // Assuming LeaveTypeID is part of the request
        };
        _unitOfWork.Context.LeaveApply.Add(leaveApply);
        _unitOfWork.Context.SaveChanges();

        // Retrieve the leave balance for the current year
        LeaveBalance leaveBalance = _unitOfWork.Context.LeaveBalance.FirstOrDefault(lb => lb.PersonId == request.EmployeeID && lb.Year == DateTime.Now.Year);
        if (leaveBalance == null)
        {
            // If no leave balance exists for the current year, create a new one
            leaveBalance = new LeaveBalance
            {
                PersonId = request.EmployeeID,
                RemainingLeave = totalLeaveAllotted,
                LastUpdated = DateTime.Now,
                Year = DateTime.Now.Year,
                AllocatedThisYear = totalLeaveAllotted,
                UsedThisYear = 0,
                LeaveApplyId = leaveApply.Id,
                LeaveTypeId = request.LeaveTypeId // Link to the created LeaveApply and LeaveType
            };
            _unitOfWork.Context.LeaveBalance.Add(leaveBalance);
        }
        else
        {
            // If leave balance exists, update the remaining leave
            leaveBalance.RemainingLeave = totalLeaveAllotted - leaveBalance.UsedThisYear;
            leaveBalance.LastUpdated = DateTime.Now;
            leaveBalance.LeaveApplyId = leaveApply.Id; // Update the LeaveApplyId
            leaveBalance.LeaveTypeId = request.LeaveTypeId; // Ensure LeaveTypeId is updated
            _unitOfWork.Context.LeaveBalance.Update(leaveBalance);
        }

        _unitOfWork.Context.SaveChanges();

        return leaveBalance;
    }

    public LeaveBalance LeaveBalanceList()
    {
        // Retrieve the leave balance for the current year
        LeaveBalance leaveBalance = _unitOfWork.Context.LeaveBalance
            .Include(x => x.Person)
            .FirstOrDefault(lb => lb.Year == DateTime.Now.Year);

        return leaveBalance;
    }

    private int GetTotalLeaveAllotted(Position position)
    {
        switch (position)
        {
            case Position.TechnicalLead:
                return 30;
            case Position.ProjectManager:
                return 30;
            case Position.ProductManager:
                return 30;
            case Position.Intern:
                return 10;
            case Position.Trainee:
                return 15;
            case Position.Junior:
                return 20;
            case Position.Senior:
                return 25;
            case Position.HRManager:
                return 30;
            default:
                throw new ArgumentException("Invalid position");
        }
    }
}
