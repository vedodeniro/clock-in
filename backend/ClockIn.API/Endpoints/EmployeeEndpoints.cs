using ClockIn.API.Data;
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
    }
}


