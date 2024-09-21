using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClientSVH.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
           
            builder.Property(u => u.Id)
              .HasColumnType("uuid")
              .ValueGeneratedOnAdd()
              .IsRequired();
            builder.Property(u => u.UserName)
                .IsRequired();
            builder.Property(u => u.PasswordHash)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired();

        }
    }
}
