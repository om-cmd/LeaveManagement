using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// Configuration class for the Person entity.
    /// </summary>
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        /// <summary>
        /// Configures the Person entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("DTbl_Person");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.UserName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(20);
            builder.Property(p => p.Address).HasMaxLength(200);
            builder.Property(p => p.Gender).HasMaxLength(10);
            builder.Property(p => p.DateOfBirth).IsRequired();
        }
    }
}
