using ClientSVH.DataAccess.Configurations;
using ClientSVH.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
    public class MyAppDbContextFactory : IDesignTimeDbContextFactory<ClientSVHDbContext>
    {
        public ClientSVHDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientSVHDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;User ID=postgres;Password=studadmin;Port=5432;Database=svhdb;");
            var b = optionsBuilder.Options;

            return new ClientSVHDbContext(b);
        }
    }
}
