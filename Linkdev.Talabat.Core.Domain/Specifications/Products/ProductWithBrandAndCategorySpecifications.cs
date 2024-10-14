using System.Runtime.CompilerServices;
using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int? pageSize, int? pageIndex, string? search) : base()
        {

            Criteria = P => (search == null || P.NormalizedName.Contains(search)) && (!brandId.HasValue || brandId == P.BrandId) && (!categoryId.HasValue || categoryId == P.CategoryId); 
           
            AddIncludes();

                switch(sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }

            if(pageSize.HasValue && pageIndex.HasValue)
                ApplyPagination(pageSize.Value * (pageIndex.Value - 1), pageSize.Value);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        protected override void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
