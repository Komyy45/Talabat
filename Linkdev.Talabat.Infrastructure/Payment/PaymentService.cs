using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Basket;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using TalabatProduct = Linkdev.Talabat.Core.Domain.Entities.Products.Product;
using Stripe;
using Microsoft.Extensions.Options;

namespace Linkdev.Talabat.Infrastructure.Payment
{
	internal class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IOptions<RedisSettings> redisSettings) : IPaymentService
	{
		private RedisSettings _redisSettings = redisSettings.Value;

		public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
		{
			// 1. Get Customer basket 
			var basket = await basketRepository.GetAsync(basketId);

			if (basket is null) throw new NotFoundException(nameof(CustomerBasket), basketId);

			// 2. Check for the Shipping Price 
			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);				 
				
				if(deliveryMethod is null) throw new NotFoundException(nameof (DeliveryMethod), basket.DeliveryMethodId);

				basket.ShippingPrice = deliveryMethod.Cost;
			}

			// 3. Check for the Products price
			if(basket.Items.Any())
			{
				var productRepo = unitOfWork.GetRepository<TalabatProduct, int>();
				foreach (var product in basket.Items)
				{
					var item = await productRepo.GetAsync(product.Id);
					if (item is null) throw new NotFoundException(nameof(TalabatProduct), product.Id);
					product.Price = item.Price;
				}
			}

			PaymentIntentService paymentIntentService = new();

			// 4. Create or Update Payment Intent 
			if (basket.PaymentIntentId is null)
			{
				PaymentIntentCreateOptions paymentIntent = new()
				{
					Amount = (long)basket.Items.Sum(p => p.Price * 100 * p.Quantity) + (long) (basket?.ShippingPrice ?? 0) * 100,
					Currency = "USD",
					PaymentMethodTypes = { "Card" }
				};

				var createdPaymentIntent = await paymentIntentService.CreateAsync(paymentIntent);
			
				basket!.PaymentIntentId = createdPaymentIntent.Id;
				basket!.PaymentIntentId = createdPaymentIntent.ClientSecret;
			}
			else
			{
				PaymentIntentUpdateOptions paymentIntent = new()
				{
					Amount = (long)basket.Items.Sum(p => p.Price * 100 * p.Quantity) + (long)(basket?.ShippingPrice ?? 0) * 100,
				};

				var updatedPaymentIntent = await paymentIntentService.UpdateAsync(basket!.PaymentIntentId, paymentIntent);
			}

			await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(_redisSettings.timeToLiveInDays));

			return basket;
		}
	}
}
