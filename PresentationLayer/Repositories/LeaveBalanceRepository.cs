using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{/// <summary>
/// to Calculate leave avaialable or check the information 
/// </summary>
 
    public class LeaveBalanceRepository : ILeaveBalanceRepo
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// injection of unitof work 
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations</param>
        public LeaveBalanceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       /// <summary>
       /// calculates the leave informations
       /// </summary>
       /// <param name="request">takes the view model params for calculations</param>
       /// <returns></returns>
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

      /// <summary>
      /// leave balance Sheet
      /// </summary>
      /// <returns>the lsit of the employee who have taken leave</returns>
        public LeaveBalance LeaveBalanceList()
        {
            // Retrieve the leave balance for the current year
            LeaveBalance leaveBalance = _unitOfWork.Context.LeaveBalance
                .Include(x => x.Person)
                .FirstOrDefault(lb => lb.Year == DateTime.Now.Year);

            return leaveBalance;
        }

        /// <summary>
        /// Determines the total leave allotted based on the position.
        /// </summary>
        /// <param name="position">The position of the employee.</param>
        /// <returns>The total leave allotted for the position.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid position is provided.</exception>
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
