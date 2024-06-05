using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class LeaveCalculationRequest
    {
        [ForeignKey(nameof(LeaveType))]
        public int LeaveTypeId {  get; set; }
        public LeaveType LeaveType { get; set; }
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }
    }
}
