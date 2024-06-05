using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    /// <summary>
    /// This is the LeaveBalance model.
    /// </summary>
    public class LeaveBalance
    {
        [Key]
        public int Id { get; set; }

        public int LeaveDaysApplied { get; set; }
        public int RemainingLeave { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Year { get; set; }
        public int AllocatedThisYear { get; set; }
        public int UsedThisYear { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Employee Person { get; set; }

        [ForeignKey(nameof(LeaveApply))]
        public int LeaveApplyId { get; set; }
        public LeaveApply LeaveApply { get; set; }

        [ForeignKey(nameof(LeaveType))]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }


    }
}