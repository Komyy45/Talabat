using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.APIs.Controllers.Errors;
using Linkdev.Talabat.APIs.Controllers.Filters;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productSpecs)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(productSpecs);
            return Ok(products);
        }

		[Cached(300)]
		[HttpGet("{id:int}")] // GET : BaseUrl/api/Products/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is { })
                return Ok(product);

            return NotFound(new ApiResponse(404, $"The Product with Id:{id} is not found!"));
        }

		[Cached(1200)]
		[HttpGet("Brands")] // BaseUrl/api/Products/Brands
        public async Task<ActionResult<BrandDto>> GetBrands()
            => Ok(await serviceManager.ProductService.GetBrandsAsync());

		[Cached(1200)]
		[HttpGet("Categories")] // BaseUrl/api/Products/Categories
        public async Task<ActionResult<CategoryDto>> GetCategories()
            => Ok(await serviceManager.ProductService.GetCategoriesAsync());
    }
}
