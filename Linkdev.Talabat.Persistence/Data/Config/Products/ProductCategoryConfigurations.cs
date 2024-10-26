
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Data.Config.Products
{
    [DbContext(typeof(StoreDbContext))]
    internal class ProductCategoryConfigurations : BaseAuditableEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                   .IsRequired();
        }
    }
}
