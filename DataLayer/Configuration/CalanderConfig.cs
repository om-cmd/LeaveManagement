using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the Calander entity.
    /// </summary>
    public class CalanderConfig : IEntityTypeConfiguration<Calander>
    {
        /// <summary>
        /// Configures the Calander entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
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
        }
    }
}
