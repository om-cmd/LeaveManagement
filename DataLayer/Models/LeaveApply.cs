using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LeaveManagement.Models;

namespace DomainLayer.Models
{
    public class LeaveApply
    {
        [Key]
        public int Id { get; set; }

        public bool LeaveApplyEnabled { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }

        [ForeignKey(nameof(LeaveBalance))]
        public int LeaveBalanceId { get; set; }
        public LeaveBalance LeaveBalance { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey(nameof(LeaveType))]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
