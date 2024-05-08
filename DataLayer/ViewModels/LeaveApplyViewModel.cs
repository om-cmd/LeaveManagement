using DomainLayer.Models;

namespace PresentationLayer.ViewModels
{
    public class LeaveApplyViewModel
    {
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public int LeavebalanceID { get; set; }
        public bool LeaveApplyEnabled { get; set; }
        public int LeaveDaysApplied { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }

        public Position Position { get; set; }



    }
}
