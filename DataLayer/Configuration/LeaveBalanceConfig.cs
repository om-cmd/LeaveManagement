using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class LeaveBalanceConfig : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.ToTable("DTbl_LeaveBalances"); // Set the table name
            builder.HasKey(lb => lb.LeaveBalanceID); // Set the primary key
            builder.Property(lb => lb.LeaveBalanceID).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(lb => lb.LeaveDaysApplied).IsRequired();
            builder.Property(lb => lb.RemainingLeave).IsRequired();
            builder.Property(lb => lb.LastUpdated).IsRequired();
            builder.Property(lb => lb.Year).IsRequired();
            builder.Property(lb => lb.AllocatedThisYear).IsRequired();
            builder.Property(lb => lb.UsedThisYear).IsRequired();

            // Configure the relationships with other entities
            builder.HasOne(lb => lb.Employee) // LeaveBalance has one Employee
                .WithMany(e => e.LeaveBalances) // Employee can have many LeaveBalance instances
                .HasForeignKey(lb => lb.EmployeeID) // Specify the foreign key property in LeaveBalance
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior

            builder.HasOne(lb => lb.LeaveApply) // LeaveBalance has one LeaveApply
               .WithOne(la => la.LeaveBalance) // LeaveApply can be associated with only one LeaveBalance
               .HasForeignKey<LeaveBalance>(lb => lb.LeaveApplyID) // Specify the foreign key property in LeaveBalance
               .IsRequired(); // Make the relationship required
        }
    }
}
