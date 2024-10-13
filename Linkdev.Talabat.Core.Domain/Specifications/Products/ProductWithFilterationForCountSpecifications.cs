using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId, string? search)
        {
            Criteria = P => (search == null || P.NormalizedName.Contains(search)) && (!brandId.HasValue || brandId == P.BrandId) && (!categoryId.HasValue || categoryId == P.CategoryId);
        }
    }
}
