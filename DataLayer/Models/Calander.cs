
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class Calander
    {
        [Key]
        public int HolidayCalendarID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Employee))]

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

}
