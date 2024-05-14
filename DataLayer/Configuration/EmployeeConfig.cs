using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("DTbl_Employee"); // Set the table name
            builder.HasKey(e => e.Id); // Set the primary key
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(e => e.Position).IsRequired(); // Configure other properties
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.JoinedDate).IsRequired();
            builder.Property(e => e.LeftDate);

            // Configure navigation properties
            builder.HasMany(e => e.LeaveBalances) // Employee has many LeaveBalance instances
                   .WithOne(lb => lb.Employee) // LeaveBalance belongs to one Employee
                   .HasForeignKey(lb => lb.EmployeeId) // Specify the foreign key property in LeaveBalance
                   .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior

            builder.HasMany(e => e.LeaveApply) // Employee has many LeaveApply instances
                   .WithOne(la => la.Employee) // LeaveApply belongs to one Employee
                   .HasForeignKey(la => la.EmployeeId) // Specify the foreign key property in LeaveApply
                   .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior

            builder.HasMany(e => e.Users) // Employee has many User instances
                   .WithOne(u => u.Employee) // User belongs to one Employee
                   // Specify the foreign key property in User
                   .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior
        }
    }
}
