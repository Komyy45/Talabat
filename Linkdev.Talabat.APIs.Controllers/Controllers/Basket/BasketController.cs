using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Basket
{
	public class BasketController(IBasketService basketService) : BaseApiController
    {
        [HttpGet] // GET: /api/Basket?id=ID
        public async Task<IActionResult> GetBasket(string id)
        {
           return Ok(await basketService.GetCustomerBasket(id));
        }

        [HttpPost] // POST : /api/Basket
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto basket)
        {
            return Ok (await basketService.UpdateBasketAsync(basket));
        }

        [HttpDelete] // Delete /api/Basket?id=ID
        public async Task DeleteBasket(string id)
        {
            await basketService.DeleteCustomerBasket(id);
        }
    }
}
