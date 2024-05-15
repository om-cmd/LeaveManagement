using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("DTbl_Employee"); 
            builder.HasKey(e => e.Id); 
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired(); 
            builder.Property(e => e.Position).IsRequired(); 
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.JoinedDate).IsRequired();
            builder.Property(e => e.LeftDate);

            builder.HasMany(e => e.LeaveBalances) 
                   .WithOne(lb => lb.Employee) 
                   .HasForeignKey(lb => lb.EmployeeId) 
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(e => e.LeaveApply) 
                   .WithOne(la => la.Employee) 
                   .HasForeignKey(la => la.EmployeeId) 
                   .OnDelete(DeleteBehavior.Cascade); 

          
        }
    }
}
