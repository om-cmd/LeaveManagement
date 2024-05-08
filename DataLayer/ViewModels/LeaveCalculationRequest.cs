using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class LeaveCalculationRequest
    {
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        public DateTime AppliedFromDate { get; set; }

        public DateTime AppliedToDate { get; set; }
    }
}
