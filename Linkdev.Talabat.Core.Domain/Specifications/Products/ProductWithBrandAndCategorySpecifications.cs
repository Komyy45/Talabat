using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort) : base()
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
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

            }
            else AddOrderBy(P => P.Name);
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
