using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class LeaveBalanceConfig : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.ToTable("DTbl_LeaveBalances"); 
            builder.HasKey(lb => lb.Id); 
            builder.Property(lb => lb.Id).ValueGeneratedOnAdd().IsRequired(); 
            builder.Property(lb => lb.LeaveDaysApplied).IsRequired(); 
            builder.Property(lb => lb.RemainingLeave).IsRequired();
            builder.Property(lb => lb.LastUpdated).IsRequired();
            builder.Property(lb => lb.Year).IsRequired();
            builder.Property(lb => lb.AllocatedThisYear).IsRequired();
            builder.Property(lb => lb.UsedThisYear).IsRequired();

            builder.HasOne(lb => lb.Employee) 
                .WithMany(e => e.LeaveBalances) 
                .HasForeignKey(lb => lb.EmployeeId) 
                .OnDelete(DeleteBehavior.Cascade); 

           

             builder.HasOne(la => la.LeaveApply)
                .WithMany(e=>e.LeaveBalances)
                .HasForeignKey(la => la.LeaveApplyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
