using Microsoft.EntityFrameworkCore;
using Pharma.Entities;

namespace Pharma.EntityFramework
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sales> Sales { get; set; }
    }
}