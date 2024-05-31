namespace DomainLayer.Models
{
    /// <summary>
    /// theses are the enum classes 
    /// </summary>
    public enum Roles
    {
        SuperAdmin,
        Admin,
        User
    }
    /// <summary>
    /// this is for the employee position
    /// </summary>
    public enum Position
    {
        TechnicalLead,
        ProjectManager,
        ProductManager,
        Intern,
        Trainee,
        Junior,
        Senior,
        HRManager
    }
    /// <summary>
    /// this is for the approval of the leave applied 
    /// </summary>
    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }
    /// <summary>
    /// this is the active status for user and employee
    /// </summary>
 
    public enum Status
    {
        Active,
        InActive
    }

}
