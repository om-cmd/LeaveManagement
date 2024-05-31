using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.Configuration
{
    /// <summary>
    /// used to set the properties of user before migration is performed 
    /// </summary>
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// configuring the user
        /// </summary>
        /// <param name="builder">the entity type builder </param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("DTbl_User");
    
        }
    }
}
