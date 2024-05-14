using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class CalanderConfig : IEntityTypeConfiguration<Calander>
    {
        public void Configure(EntityTypeBuilder<Calander> builder)
        {
            builder.ToTable("DTbl_Calander");
            builder.HasKey(x => x.HolidayCalendarID);
            builder.Property(x => x.HolidayCalendarID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.IsPublicHoliday).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Region).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .IsRequired(false);
        }
    }
}
