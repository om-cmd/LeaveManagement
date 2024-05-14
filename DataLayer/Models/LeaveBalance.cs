using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class LeaveBalance
    {
        [Key]
        public int Id { get; set; }
        
        public int LeaveDaysApplied {  get; set; }
        public int RemainingLeave { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Year { get; set; }
        public int AllocatedThisYear { get; set; }
        public int UsedThisYear { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<LeaveApply> Leaves { get; set; }

    }
}
