
namespace LeaveManagement.Models
{
    public class Calander
    {
        public int HolidayCalendarID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }

}
