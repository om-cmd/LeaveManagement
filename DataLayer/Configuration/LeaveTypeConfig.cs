using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("DTbl_LeaveTypes");
            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(lt => lt.Name).HasMaxLength(100).IsRequired();

            //builder.HasMany(lt => lt.LeaveApply)
            //    .WithOne(la => la.LeaveType)
            //    .HasForeignKey(la => la.LeaveTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
