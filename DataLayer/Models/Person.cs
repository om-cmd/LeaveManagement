using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
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

    }
    public enum Gender
    {
        Male,
        Female,
        Others
    }
}