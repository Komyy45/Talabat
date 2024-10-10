using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Core.Domain.Specifications;
using Linkdev.Talabat.Core.Domain.Specifications.Products;

namespace Linkdev.Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            return mapper.Map<IEnumerable<ProductToReturnDto>>(products);
        }

        public async Task<ProductToReturnDto?> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec, id);

            return mapper.Map<ProductToReturnDto>(product);
        }

        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            return mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
