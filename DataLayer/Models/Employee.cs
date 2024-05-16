using DomainLayer.Models;
using System;
using System.Collections.Generic;

namespace LeaveManagement.Models
{
    public class Employee : Person
    {
        public Position Position { get; set; }
        public Status Status { get; set; }
        public DateTime? LeftDate { get; set; }
        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; }
    }
}