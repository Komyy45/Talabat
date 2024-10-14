using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Core.Domain.Specifications.Products;

namespace Linkdev.Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams productSpecs)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productSpecs.Sort, productSpecs.BrandId, productSpecs.CategoryId, productSpecs.PageSize, productSpecs.PageIndex, productSpecs.Search);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var mappedProducts = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpecifications = new ProductWithFilterationForCountSpecifications(productSpecs.BrandId, productSpecs.CategoryId, productSpecs.Search);
            var count = await unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpecifications);

            return new Pagination<ProductToReturnDto>() { PageIndex = productSpecs.PageIndex, PageSize = productSpecs.PageSize, Count = count, Data = mappedProducts };
        }

        public async Task<ProductToReturnDto?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec, id);

            return mapper.Map<ProductToReturnDto>(product);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            return mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
