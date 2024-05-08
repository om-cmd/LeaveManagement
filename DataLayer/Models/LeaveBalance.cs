namespace LeaveManagement.Models
{
    public class LeaveBalance
    {
        public int LeaveBalanceID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveApplyID { get; set; }
        
        public int LeaveDaysApplied {  get; set; }
        public int RemainingLeave { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Year { get; set; }
        public int AllocatedThisYear { get; set; }
        public int UsedThisYear { get; set; }

        public Employee Employee { get; set; }
        public LeaveApply LeaveApply { get; set; }

        public ICollection<LeaveApply> Leaves { get; set; }

    }
}
