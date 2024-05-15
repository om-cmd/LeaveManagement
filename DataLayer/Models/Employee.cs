using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public Position Position { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Status Status {get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.Now;

        public DateTime? LeftDate { get; set; }

        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; } 

    }
   
}
