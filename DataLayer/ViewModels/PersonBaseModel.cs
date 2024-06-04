
using BusinessLayer.AttributeValidations;
using DomainLayer.Models;

namespace DomainLayer.ViewModels
{
    public class PersonBaseModel
    {
        public string UserName { get; set; }
        [ValidatePassword]

        public string Password { get; set; }
        [ValidateEmail]

        public string Email { get; set; }
        [ValidatePhone]

        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Position Position { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        public Roles Roles { get; set; }


    }
}
