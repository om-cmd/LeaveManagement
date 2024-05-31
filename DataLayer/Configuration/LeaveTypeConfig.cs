using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the LeaveType entity.
    /// </summary>
    public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
    {
        /// <summary>
        /// Configures the LeaveType entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("DTbl_LeaveTypes");
            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(lt => lt.Name).HasMaxLength(100).IsRequired();
        }
    }
}
