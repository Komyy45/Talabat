using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] // GET: /api/Basket?id=ID
        public async Task<IActionResult> GetBasket(string id)
        {
           return Ok(await serviceManager.BasketService.GetCustomerBasket(id));
        }

        [HttpPost] // POST : /api/Basket
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto basket)
        {
            return Ok (await serviceManager.BasketService.UpdateBasketAsync(basket));
        }

        [HttpDelete] // Delete /api/Basket?id=ID
        public async Task DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteCustomerBasket(id);
        }
    }
}
