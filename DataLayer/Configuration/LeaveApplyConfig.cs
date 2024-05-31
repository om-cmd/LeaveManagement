using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainLayer.Models;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the LeaveApply entity.
    /// </summary>
    public class LeaveApplyConfig : IEntityTypeConfiguration<LeaveApply>
    {
        /// <summary>
        /// Configures the LeaveApply entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<LeaveApply> builder)
        {
            builder.ToTable("DTbl_LeaveApply");
            builder.HasKey(la => la.Id);
            builder.Property(la => la.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(la => la.LeaveApplyDescription).IsRequired();
            builder.Property(la => la.LeaveApplyEnabled).IsRequired();
            builder.Property(la => la.AppliedFromDate).IsRequired();
            builder.Property(la => la.AppliedToDate).IsRequired();
        }
    }
}
