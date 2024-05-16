using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainLayer.Models;

namespace DomainLayer.Configuration
{
    public class LeaveApplyConfig : IEntityTypeConfiguration<LeaveApply>
    {
        public void Configure(EntityTypeBuilder<LeaveApply> builder)
        {
            builder.ToTable("DTbl_LeaveApply");
            builder.HasKey(la => la.Id);
            builder.Property(la => la.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(la => la.LeaveApplyDescription).IsRequired();
            builder.Property(la => la.LeaveApplyEnabled).IsRequired();
            builder.Property(la => la.AppliedFromDate).IsRequired();
            builder.Property(la => la.AppliedToDate).IsRequired();

            //builder.HasOne(la => la.Employee)
            //     .WithMany() 
            //     .HasForeignKey(la => la.EmployeeId)
            //     .IsRequired()
            //     .OnDelete(DeleteBehavior.Restrict)
            //     .HasConstraintName("FK_LeaveApplies_Person");


            //builder.HasOne(la => la.LeaveType)
            //    .WithMany(lt => lt.LeaveApply)
            //    .HasForeignKey(la => la.LeaveTypeId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasConstraintName("FK_LeaveApplies_LeaveTypes");

         


        }
    }
}
