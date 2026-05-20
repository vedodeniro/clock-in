using Microsoft.EntityFrameworkCore;
using ClockIn.API.Models;

namespace ClockIn.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
    }
}