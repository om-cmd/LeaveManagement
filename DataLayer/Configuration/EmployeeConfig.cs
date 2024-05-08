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
            builder.HasKey(x => x.EmployeeID);
            builder.Property(x => x.EmployeeID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.EmployeeName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ContactDetails).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Position).HasMaxLength(50).IsRequired();
            builder.Property(x => x.JoinedDate).IsRequired();
            builder.Property(x => x.LeftDate).IsRequired();

          

            builder.HasMany(e => e.LeaveBalances)
                .WithOne(lb => lb.Employee)
                .HasForeignKey(lb => lb.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.LeaveApply)
                .WithOne(la => la.Employee)
                .HasForeignKey(la => la.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
