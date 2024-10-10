﻿using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:int}")] // GET : BaseUrl/api/Products/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProduct(id);

            if (product is { })
                return Ok(product);

            return NotFound();
        }
    }
}
