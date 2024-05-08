namespace LeaveManagement.Models
{
    public class LeaveType
    {
        public int LeaveTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsLeave {  get; set; }  

        public ICollection<LeaveApply> LeaveApply { get; set; }


    }
}
