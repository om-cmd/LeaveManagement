using DomainLayer.Configuration;
using DomainLayer.Models;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Data
{
    /// <summary>
    /// Represents the database context for Leave Management.
    /// </summary>
    public class LeaveDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for the database context.</param>
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the DbSet for the Employee entity.
        /// </summary>
        public DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Calander entity.
        /// </summary>
        public DbSet<Calander> HolidayCalanders { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the LeaveApply entity.
        /// </summary>
        public DbSet<LeaveApply> LeaveApply { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the LeaveBalance entity.
        /// </summary>
        public DbSet<LeaveBalance> LeaveBalance { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the LeaveType entity.
        /// </summary>
        public DbSet<LeaveType> LeaveType { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the User entity.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Notification entity.
        /// </summary>
        public DbSet<Notification> Notification { get; set; }

        /// <summary>
        /// Configures the model for the database context.
        /// </summary>
        /// <param name="builder">The model builder instance being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure Notification Id property to be generated on add
            builder.Entity<Notification>()
                   .Property(n => n.Id)
                   .ValueGeneratedOnAdd();

            // Apply entity configurations
            builder.ApplyConfiguration(new EmployeeConfig());
            builder.ApplyConfiguration(new CalanderConfig());
            builder.ApplyConfiguration(new LeaveTypeConfig());
            builder.ApplyConfiguration(new LeaveApplyConfig());
            builder.ApplyConfiguration(new LeaveBalanceConfig());
            builder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(builder);
        }
    }
}
