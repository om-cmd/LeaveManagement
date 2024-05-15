using DomainLayer.Models;
using LeaveManagement.Models;

namespace PresentationLayer.VIewModels
{
    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Position Position { get; set; }

    }
}
