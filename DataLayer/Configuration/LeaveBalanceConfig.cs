using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the LeaveBalance entity.
    /// </summary>
    public class LeaveBalanceConfig : IEntityTypeConfiguration<LeaveBalance>
    {
        /// <summary>
        /// Configures the LeaveBalance entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
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

            // Define the relationship with LeaveApply
            builder.HasOne(la => la.LeaveApply)
                   .WithMany(e => e.LeaveBalances)
                   .HasForeignKey(la => la.LeaveApplyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
