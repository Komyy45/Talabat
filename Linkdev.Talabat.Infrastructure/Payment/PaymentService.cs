using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Payment;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Basket;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Linkdev.Talabat.Core.Domain.Specifications.Orders;
using Linkdev.Talabat.Infrastructure.Payment.options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using TalabatProduct = Linkdev.Talabat.Core.Domain.Entities.Products.Product;

namespace Linkdev.Talabat.Infrastructure.Payment
{
	internal class PaymentService(IBasketRepository basketRepository, 
		IUnitOfWork unitOfWork, 
		IMapper mapper, 
		IOptions<RedisSettings> redisSettings, 
		IOptions<StripeSettings> stripeSettings,
		ILogger<PaymentService> logger) : IPaymentService
	{
		private RedisSettings _redisSettings = redisSettings.Value;
		private StripeSettings _stripeSettings = stripeSettings.Value;

		public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

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
					PaymentMethodTypes = new() { "card" }
				};

				var createdPaymentIntent = await paymentIntentService.CreateAsync(paymentIntent);
			
				basket!.PaymentIntentId = createdPaymentIntent.Id;
				basket!.ClientSecret = createdPaymentIntent.ClientSecret;
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

			return mapper.Map<CustomerBasketDto>(basket);
		}

		public async Task UpdateOrderStatus(string requestBody, string header)
		{
			var stripeEvent =  EventUtility.ConstructEvent(requestBody, header, _stripeSettings.WebHookSecret);

			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
			Order order = null!;
			switch(stripeEvent.Type)
			{
				case "payment_intent.payment_failed":
					order = await SetOrderStatus(paymentIntent!.Id, OrderStatus.PaymentFailed);
					break;
				case "payment_intent.succeeded":
					order = await SetOrderStatus(paymentIntent!.Id, OrderStatus.PaymentReceived);
					break;
				default:
					logger.LogError("Unhandled event type: {0}", stripeEvent.Type);
					break;
			}

			await unitOfWork.CompleteAsync();

			if (order is not null)
				logger.LogInformation($"Order with Payment intent {stripeEvent.Id} Status is {order.Status}");
		}

		private async Task<Order> SetOrderStatus(string paymentIntent, OrderStatus orderStatus)
		{
			var ordersRepository = unitOfWork.GetRepository<Order, int>();

			OrderSpecifications spec = new OrderSpecifications(paymentIntent, false);
			var order = await ordersRepository.GetAllAsync(spec);

			if (!order.Any()) throw new NotFoundException(nameof(Order), paymentIntent);

			var o = order.SingleOrDefault();
			o!.Status = orderStatus;

			ordersRepository.Update(o);

			return o;
		}
	}
}
