using Microsoft.EntityFrameworkCore;
using SchedulingPatients.MVC.Models;

namespace SchedulingPatients.MVC.Data
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<ServiceAssignment> ServiceAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<ServiceAssignment>().ToTable("ServiceAssignment");

            modelBuilder.Entity<ServiceAssignment>()
                .HasKey(c => new { c.ServiceID, c.EmployeeID });
        }
    }
}
