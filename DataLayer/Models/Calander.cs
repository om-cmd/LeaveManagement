
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
       
    }

}
