using DomainLayer.Configuration;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace DomainLayer.Data
{
    public class LeaveDbContext : DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Calander> HolidayCalanders { get; set; }
        public DbSet<LeaveApply> LeaveApply { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }   

        public DbSet<LeaveType> LeaveType { get; set; }

        public DbSet<User> Users { get; set; }


        //public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.ApplyConfiguration(new EmployeeConfig());
            Builder.ApplyConfiguration(new CalanderConfig());
            Builder.ApplyConfiguration(new LeaveTypeConfig());
            Builder.ApplyConfiguration(new LeaveApplyConfig());
            Builder.ApplyConfiguration(new LeaveBalanceConfig());
            Builder.ApplyConfiguration(new UserConfig());
            //Builder.ApplyConfiguration(new PersonConfig());

            base.OnModelCreating(Builder); 
        }

    }
}
