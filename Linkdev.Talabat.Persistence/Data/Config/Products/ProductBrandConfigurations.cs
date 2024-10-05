

namespace Linkdev.Talabat.Persistence.Data.Config.Products
{
    internal class ProductBrandConfigurations : BaseEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                   .IsRequired();
        }
    }
}
