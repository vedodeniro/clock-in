using ClockIn.API.Data;
using ClockIn.API.DTOs;
using ClockIn.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClockIn.API.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this WebApplication app)
    {
        app.MapGet("/employees", async (AppDbContext db) =>
        {
            return await db.Employees.ToListAsync();
        });

        app.MapPost("/employees", async (Employee employee, AppDbContext db) =>
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return Results.Created($"/employees/{employee.Id}" , employee);
        });

        app.MapGet("/employees/{id}", async (int id, AppDbContext db) =>
        {
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(employee);
        });

        app.MapGet("/employees/{EmployeeNumber}/status", async (string EmployeeNumber, AppDbContext db) =>
        {
            var employee = await db.Employees.FirstOrDefaultAsync(e => e.EmployeeNumber == EmployeeNumber);
            if (employee == null)
            {
                return Results.NotFound();
            }

            var existingEntry = await db.TimeEntries.FirstOrDefaultAsync(t => t.EmployeeId == employee.Id && t.ClockOutTime == null);

            var employeeStatusResponse = new EmployeeStatusResponse
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                IsClockedIn = existingEntry != null

            };

            return Results.Ok(employeeStatusResponse);
        });
    }
}


