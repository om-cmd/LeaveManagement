namespace PresentationLayer.ViewModels
{
    public class LeaveBalanceViewModel
    {
        public int EmployeeID { get; set; }
        public int RemainingLeave { get; set; }

        public int LeaveApplyID {  get; set; }

        public int LeaveDaysApplied {  get; set; }
        public DateTime LastUpdated { get; set; }
        public int Year { get; set; }
        public int AllocatedThisYear { get; set; }
        public int UsedThisYear { get; set; }

    }
}
