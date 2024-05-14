using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
       
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateRemoved { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
