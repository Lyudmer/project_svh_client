using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClientSVH.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.UserName)
                .IsRequired();
            builder.Property(b => b.PasswordHash)
                .IsRequired();
            builder.Property(b => b.Email)
                .IsRequired();

        }
    }
}
