using BusinessLayer.AttributeValidations;
using DomainLayer.Models;
using LeaveManagement.Models;

namespace PresentationLayer.VIewModels
{
    public class EmployeeViewModel
    {
        [ValidateEmail]
        public string Email { get; set; }
        [ValidatePhone]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public Roles Roles { get; set; }    


    }
}
