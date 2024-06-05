using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    /// <summary>
    /// this is the model of employee inherited from person base class.
    /// </summary>
    public class Employee : Person
    {
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        public Status Status { get; set; }
        public DateTime? LeftDate { get; set; }
        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; }

        public ICollection<Notification> Notification { get; set; }
    }
}