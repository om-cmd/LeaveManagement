using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class LeaveApplyConfig : IEntityTypeConfiguration<LeaveApply>
    {
        public void Configure(EntityTypeBuilder<LeaveApply> builder)
        {
            builder.ToTable("DTbl_LeaveApplies"); // Set the table name
            builder.HasKey(la => la.LeaveApplyID); // Set the primary key
            builder.Property(la => la.LeaveApplyID).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(la => la.LeaveApplyEnabled).IsRequired();
            builder.Property(la => la.AppliedFromDate).IsRequired();
            builder.Property(la => la.AppliedToDate).IsRequired();

            // Configure the relationships with other entities
            builder.HasOne(la => la.Employee) // LeaveApply has one Employee
                .WithMany(e => e.LeaveApply) // Employee can have many LeaveApply instances
                .HasForeignKey(la => la.EmployeeID) // Specify the foreign key property in LeaveApply
                .OnDelete(DeleteBehavior.Restrict); // Configure the delete behavior

            builder.HasOne(la => la.LeaveType) // LeaveApply has one LeaveType
                .WithMany(lt => lt.LeaveApply) // LeaveType can be associated with many LeaveApply instances
                .HasForeignKey(la => la.LeaveTypeID) // Specify the foreign key property in LeaveApply
                .OnDelete(DeleteBehavior.Restrict); // Configure the delete behavior

            builder.HasOne(la => la.LeaveBalance) // LeaveApply has one LeaveBalance
                .WithOne(lb => lb.LeaveApply) // LeaveBalance has one LeaveApply (one-to-one)
                .HasForeignKey<LeaveApply>(la => la.LeaveBalanceID) // Specify the foreign key property in LeaveBalance
                .IsRequired() // Make the relationship required
                .OnDelete(DeleteBehavior.Restrict); // Configure the delete behavior
        }
    }
}
