﻿using LeaveManagement.Models;

namespace DomainLayer.Models
{
    public enum Roles
    {
     Admin,
     Employee
    }
    public enum Position
    {
        Intern,
        Trainee,
        Junior,
        Senior
    }
    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public enum RequestStatus
    {
        Submitted,
        Approved,
        Rejected
    }
    public enum Status
    { 
        Active,
        InActive
    }
   
}
