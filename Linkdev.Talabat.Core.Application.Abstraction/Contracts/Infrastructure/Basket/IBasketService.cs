using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Basket
{
	public interface IBasketService
	{
		Task<CustomerBasketDto> GetCustomerBasket(string id);

		Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);

		Task DeleteCustomerBasket(string id);
	}
}
