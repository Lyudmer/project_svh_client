

using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSVH.PackagesDBCore.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder.ToTable("pkg_status");
   //ключи
            builder.HasKey(s => s.Id);
            
            //свойства полей
            builder.Property(s => s.Id)
                   .IsRequired()
                   .HasColumnName("id");
            builder.Property(s => s.StatusName)
                   .HasColumnName("stname")
                   .HasMaxLength(250);
            builder.Property(s => s.MkRes)
                   .HasColumnName("mkres")
                   .HasDefaultValue(false);
            builder.Property(s => s.RunWf)
                   .HasColumnName("runwf")
                   .HasDefaultValue(false); 
            builder.Property(s => s.SendMess)
                   .HasColumnName("sendmess")
                   .HasDefaultValue(false);
        }
    }
}
