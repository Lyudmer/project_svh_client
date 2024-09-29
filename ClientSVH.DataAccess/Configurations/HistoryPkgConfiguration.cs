using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClientSVH.DataAccess.Configurations
{
    public class HistoryPkgConfiguration : IEntityTypeConfiguration<HistoryPkgEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryPkgEntity> builder)
        {
            builder.ToTable("history_pkg");
            //ключи
            builder.HasKey(p => p.Id);

            //свойства полей
            builder.Property(p => p.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasColumnName("id")
                   .HasColumnType("uuid");

            builder.Property(p => p.CreateDate)
                   .HasColumnName("create_date")
                   .HasDefaultValueSql("now()");
            builder.Property(s => s.Oldst)
                   .HasColumnName("oldst")
                   .IsRequired();
            builder.Property(s => s.Newst)
                   .HasColumnName("newst")
                   .IsRequired();
            builder.Property(s => s.Comment)
                   .HasColumnName("comment");

        }
    }
}
