using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("DTbl_LeaveTypes"); // Set the table name
            builder.HasKey(lt => lt.LeaveTypeID); // Set the primary key
            builder.Property(lt => lt.LeaveTypeID).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(lt => lt.Name).HasMaxLength(50).IsRequired(); // Configure other properties
            builder.Property(lt => lt.Description).HasMaxLength(100);
            builder.Property(lt => lt.IsLeave).IsRequired();

            // Configure the relationship with LeaveApply entity
            builder.HasMany(lt => lt.LeaveApply)
                .WithOne(la => la.LeaveType) // LeaveApply has one LeaveType
                .HasForeignKey(la => la.LeaveTypeID) // Specify the foreign key property in LeaveApply
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior
        }
    }
}
