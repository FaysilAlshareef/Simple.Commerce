using Microsoft.EntityFrameworkCore;
using Simple.Commerce.Domain.Entities;
using Simple.Commerce.Infra.Persistence.Configurations;

namespace Simple.Commerce.Infra.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
