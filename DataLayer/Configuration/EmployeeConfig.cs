using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the Employee entity.
    /// </summary>
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Configures the Employee entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("DTbl_Employee");

            // Configure primary key (Id) inherited from Person
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

            // Other property configurations...
            builder.Property(e => e.Position).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.JoinedDate).IsRequired();
            builder.Property(e => e.LeftDate);
        }
    }
}
