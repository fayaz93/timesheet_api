using Microsoft.EntityFrameworkCore;
using System;
using timesheet.model;

namespace timesheet.data
{
    public class TimesheetDb : DbContext
    {
        public TimesheetDb(DbContextOptions<TimesheetDb> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Worklog> Worklog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worklog>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Worklogs)
                .HasForeignKey(x => x.EmployeeId)
                .IsRequired(true)
                .HasConstraintName("FK_Worklog_EmployeeId_Employee_Id");

            modelBuilder.Entity<Worklog>()
                .HasOne(x => x.Task)
                .WithMany(x => x.Worklogs)
                .HasForeignKey(x => x.TaskId)
                .HasConstraintName("FK_Worklog_TaskId_Task_Id");

            modelBuilder.Entity<Employee>().Ignore(x => x.Worklogs);
            modelBuilder.Entity<Task>().Ignore(x => x.Worklogs);
        }
    }
}
