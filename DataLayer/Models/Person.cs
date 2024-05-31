using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    /// <summary>
    /// This is the base model used for Employee and User Creation
    /// </summary>
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public Position Position { get; set; }
        public Roles Roles { get; set; }
        public string CreatedBy { get; set; } 

    }
    public enum Gender
    {
        Male,
        Female,
        Others
    }
}