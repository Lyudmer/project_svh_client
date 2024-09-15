using ClientSVH.DataAccess.Configurations;
using ClientSVH.DataAccess.Entities;
using ClientSVH.PackagesDBCore.Configurations;
using ClientSVH.PackagesDBDateAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ClientSVH.DataAccess
{
    public class ClientSVHDbContext(DbContextOptions<ClientSVHDbContext> options)
        : DbContext(options)
    {

        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<PackageEntity> Packages { get; set; }
        public DbSet<DocumentEntity> Document { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public DbSet<StatusGraphEntity> StatusGraph { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new StatusGraphConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }
}
