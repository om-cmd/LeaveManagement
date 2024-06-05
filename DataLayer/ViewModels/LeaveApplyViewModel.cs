using DomainLayer.Models;
using LeaveManagement.Models;

namespace PresentationLayer.ViewModels
{
    public class LeaveApplyViewModel
    {
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }

        
        public bool LeaveApplyEnabled { get; set; }
        public string LeaveApplyDescription { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }


    }
}
