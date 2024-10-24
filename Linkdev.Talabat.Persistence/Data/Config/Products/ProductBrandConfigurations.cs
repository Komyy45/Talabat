

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Data.Config.Products
{
    [DbContext(typeof(StoreDbContext))]
    internal class ProductBrandConfigurations : BaseAuditableEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                   .IsRequired();
        }
    }
}
