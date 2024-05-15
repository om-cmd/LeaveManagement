using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("DTbl_User"); 
            builder.HasKey(u => u.Id); 
            builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired(); 
            builder.Property(u => u.UserName).HasMaxLength(50).IsRequired(); 
            builder.Property(u => u.Password).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(20);
            builder.Property(u => u.Address).HasMaxLength(200);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.DateCreated).IsRequired();

           
        }
    }
}
