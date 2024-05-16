using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
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
            Employee employee = _unitOfWork.Context.Employee.FirstOrDefault(e => e.Id == request.EmployeeID);
            if (employee == null)
            {
                return null; 
            }

            // Get the position of the employee
            Position position = employee.Position;

            // Determine the total leave allotted based on the employee's position
            int totalLeaveAllotted = GetTotalLeaveAllotted(position);

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
                    UsedThisYear = 0
                };
                _unitOfWork.Context.LeaveBalance.Add(leaveBalance);
            }
            else
            {
                // If leave balance exists, update the remaining leave
                leaveBalance.RemainingLeave = totalLeaveAllotted - leaveBalance.UsedThisYear; // Update remaining leave based on used leave
                leaveBalance.LastUpdated = DateTime.Now;
                _unitOfWork.Context.LeaveBalance.Update(leaveBalance);
            }

            _unitOfWork.Context.SaveChanges();

            return leaveBalance; // Return the updated or created leave balance

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
