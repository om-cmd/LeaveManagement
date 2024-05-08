using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class LeaveApply
    {
        public int LeaveApplyID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }

        [ForeignKey("LeaveBalanceID")]
        public int LeaveBalanceID { get; set; }

        public bool LeaveApplyEnabled { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }

        public Position Position { get; set; }

        public LeaveBalance LeaveBalance { get; set; }

        public Employee Employee { get; set; }

        public LeaveType LeaveType { get; set; }

    }
}
