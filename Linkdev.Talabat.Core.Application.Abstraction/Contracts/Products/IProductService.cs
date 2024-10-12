using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams productSpecs);

        Task<ProductToReturnDto?> GetProductAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
