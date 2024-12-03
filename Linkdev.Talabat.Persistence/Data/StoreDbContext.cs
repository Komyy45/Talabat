using Castle.Core.Internal;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly, 
                dbContextType => dbContextType.GetAttribute<DbContextAttribute>()?.ContextType == typeof(StoreDbContext));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
