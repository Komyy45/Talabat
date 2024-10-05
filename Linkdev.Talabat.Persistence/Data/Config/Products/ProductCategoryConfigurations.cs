
namespace Linkdev.Talabat.Persistence.Data.Config.Products
{
    internal class ProductCategoryConfigurations : BaseEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                   .IsRequired();
        }
    }
}
