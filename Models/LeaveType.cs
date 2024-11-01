namespace Models
{
    public class LeaveType
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // e.g., Sick Leave, Casual Leave
        public string Description { get; set; } // Details about the leave type
        public int DefaultDays { get; set; } // Default number of days for this leave type
    }
}
