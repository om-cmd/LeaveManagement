using DomainLayer.Models;
using LeaveManagement.Models;

namespace PresentationLayer.VIewModels
{
    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string ContactDetails { get; set; }
        public string Department { get; set; }
        public Position Position { get; set; }

    }
}
