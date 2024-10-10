
namespace Linkdev.Talabat.Persistence.Data.Config.Products
{
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
