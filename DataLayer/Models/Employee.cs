using DomainLayer.Models;

namespace LeaveManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string ContactDetails { get; set; }
        public Position Position { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Status Status {get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.Now;

        public DateTime? LeftDate { get; set; }

        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; }
        

    }
   
}
