﻿using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(string? sort)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(sort);
            return Ok(products);
        }

        [HttpGet("{id:int}")] // GET : BaseUrl/api/Products/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is { })
                return Ok(product);

            return NotFound();
        }

        [HttpGet("Brands")] // BaseUrl/api/Products/Brands
        public async Task<ActionResult<BrandDto>> GetBrands()
            => Ok(await serviceManager.ProductService.GetBrandsAsync());


        [HttpGet("Categories")] // BaseUrl/api/Products/Categories
        public async Task<ActionResult<CategoryDto>> GetCategories()
            => Ok(await serviceManager.ProductService.GetCategoriesAsync());
    }
}
