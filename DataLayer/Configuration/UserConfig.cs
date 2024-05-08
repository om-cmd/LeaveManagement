using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("DTbl_Users"); // Set the table name
            builder.HasKey(u => u.UserID); // Set the primary key
            builder.Property(u => u.UserID).ValueGeneratedOnAdd().IsRequired(); // Configure the primary key property
            builder.Property(u => u.UserName).HasMaxLength(50).IsRequired(); // Configure other properties
            builder.Property(u => u.Password).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(20);
            builder.Property(u => u.Address).HasMaxLength(200);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.DateCreated).IsRequired();
            builder.Property(u => u.DateRemoved);
        }
    }
}
