﻿using DomainLayer.Models;

namespace LeaveManagement.Models
{
    public class Employee : Person
    {
        public Status Status { get; set; }
        public DateTime? LeftDate { get; set; }
        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<LeaveApply> LeaveApply { get; set; }

        public ICollection<Notification> Notification { get; set; }
    }
}