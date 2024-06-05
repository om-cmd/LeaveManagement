using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    /// <summary>
    /// This is the model of leaveType 
    /// </summary>
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLeave { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; }
        public ICollection<LeaveType> LeaveTypes { get; set; }



    }
}
