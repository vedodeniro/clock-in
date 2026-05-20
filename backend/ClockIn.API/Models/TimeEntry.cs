namespace ClockIn.API.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}