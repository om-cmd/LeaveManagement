using LeaveManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the LeaveApply model.
    /// </summary>
    public class LeaveApply
    {
        [Key]
        public int Id { get; set; }

        public bool LeaveApplyEnabled { get; set; }

        public string LeaveApplyDescription { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }

        public ApprovalStatus Status { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey(nameof(LeaveType))]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public ICollection<LeaveBalance> LeaveBalances { get; set; }
    }
}
