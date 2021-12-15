using EcommerceAPI.Entities;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext()
        {

        }
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property<decimal>(p => p.UnitPrice)
                .HasPrecision(19, 6);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
