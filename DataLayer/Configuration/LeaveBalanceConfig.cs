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
            builder.HasKey(lb => lb.Id); // Set the primary key
            builder.Property(lb => lb.Id).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(lb => lb.LeaveDaysApplied).IsRequired(); // Configure other properties
            builder.Property(lb => lb.RemainingLeave).IsRequired();
            builder.Property(lb => lb.LastUpdated).IsRequired();
            builder.Property(lb => lb.Year).IsRequired();
            builder.Property(lb => lb.AllocatedThisYear).IsRequired();
            builder.Property(lb => lb.UsedThisYear).IsRequired();

            // Configure foreign key relationship
            builder.HasOne(lb => lb.Employee) // LeaveBalance belongs to one Employee
                .WithMany(e => e.LeaveBalances) // Employee can have many LeaveBalance instances
                .HasForeignKey(lb => lb.EmployeeId) // Specify the foreign key property in LeaveBalance
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior

            // Configure the navigation property
            builder.HasMany(lb => lb.Leaves) // LeaveBalance has many LeaveApply instances
                .WithOne(la => la.LeaveBalance) // LeaveApply belongs to one LeaveBalance
                .HasForeignKey(la => la.LeaveBalanceId) // Specify the foreign key property in LeaveApply
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior
        }
    }
}
