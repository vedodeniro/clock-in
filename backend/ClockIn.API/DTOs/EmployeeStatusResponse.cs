namespace ClockIn.API.DTOs;

public class EmployeeStatusResponse
{
    public int EmployeeId { get; set; }
    public string Name {get;set; } = string.Empty;
    public bool IsClockedIn {get; set; }
}