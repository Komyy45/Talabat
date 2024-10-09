using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProducts()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            return mapper.Map<IEnumerable<ProductToReturnDto>>(products);
        }

        public async Task<ProductToReturnDto?> GetProduct(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

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
