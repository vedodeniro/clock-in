using ClockIn.API.Data;
using ClockIn.API.Models;
using Microsoft.EntityFrameworkCore;


namespace ClockIn.API.Endpoints;

public static class TimeEntryEndpoints
{
    public static void MapTimeEntryEndpoints(this WebApplication app)
    {
        app.MapPost("/clock-in/{EmployeeId}", async (int EmployeeId, AppDbContext db) =>
        {
            var employee = await db.Employees.FindAsync(EmployeeId);
            if (employee == null)
            {
                return Results.NotFound();
            }

            var existingEntry = await db.TimeEntries.FirstOrDefaultAsync(t => t.EmployeeId == employee.Id && t.ClockOutTime == null);

            if (existingEntry != null)
            {
                return Results.BadRequest("Employee is already clocked in");
            }

            var timeEntry = new TimeEntry
            {
                EmployeeId = employee.Id,
                ClockInTime = DateTime.UtcNow,
            };
                db.TimeEntries.Add(timeEntry);
                await db.SaveChangesAsync();
                return Results.Created($"/clock-in/{timeEntry.Id}", timeEntry);
        });

        app.MapPost("/clock-out/{EmployeeId}", async (int EmployeeId, AppDbContext db) =>
        {
            var employee = await db.Employees.FindAsync(EmployeeId);
            if (employee == null)
            {
                return Results.NotFound();
            }

            var existingEntry = await db.TimeEntries.FirstOrDefaultAsync(t => t.EmployeeId == employee.Id && t.ClockOutTime == null);

            if (existingEntry == null)
            {
                return Results.BadRequest("Employee is not clocked in");
            }
            
                existingEntry.ClockOutTime = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return Results.Ok(existingEntry);

        });
    }
}
