
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSVH.DataAccess.Configurations
{
    public class StatusGraphConfiguration : IEntityTypeConfiguration<StatusGraphEntity>
    {
        
        public void Configure(EntityTypeBuilder<StatusGraphEntity> builder)
        {
            builder.ToTable("pkg_status_graph");
    //ключи
            builder.HasKey(st => st.OldSt);
            builder
                .HasOne(s => s.Status)
                .WithOne(st => st.StatusGraph)
                .HasForeignKey<StatusEntity>(st => st.OldSt);
            //свойства полей
            builder.Property(st => st.OldSt)
                   .HasColumnName("oldst")
                   .IsRequired();
            builder.Property(st => st.NewSt)
                    .HasColumnName("newst")
                   .IsRequired();
           
        }
    }
}
