using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Orders;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Orders;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Application.Services.Orders
{
	public class OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo, IMapper mapper) : IOrderService
	{
		public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, CreatedOrderDto createdOrder)
		{
			var order = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = mapper.Map<Address>(createdOrder.Address),
				DeliveryMethodId = createdOrder.DeliveryMethodId,
			};

			// 1. Get Basket
			var basket = await basketRepo.GetAsync(createdOrder.BasketId);

			var items = new List<OrderItem>();

			// 2. Get Products Selected into the basket
			order.Items = (ICollection<OrderItem>)basket!.Items.Select(
				async item =>
				{
					var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);

					if (product is null)
						throw new NotFoundException("Product Not Found!", item.Id);

					var createdOrderItem = new OrderItem()
					{
						Product = new ProductOrderItem()
						{
							ProductId = item.Id,
							ProductName = product.Name,
							PictureUrl = product.PictureUrl,
						},
						Price = product.Price,
						Quantity = item.Quantity,
					};

					// 3. Calculate Subtotal
					order.SubTotal += (product.Price * item.Quantity);

					return createdOrderItem;
				}
			);

			// 4. Saving changes to the database
			await unitOfWork.GetRepository<Order, int>().AddAsync(order);

			var rows = await unitOfWork.CompleteAsync();

			if (rows <= 0) throw new BadRequestException("An Error has been Occured while Creating the Order");

			return mapper.Map<OrderToReturnDto>(order);
		}

		public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<OrderToReturnDto> GetOrderByIdAsync(string clientEmail, int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<OrderToReturnDto>> GetUserOrdersAsync()
		{
			throw new NotImplementedException();
		}
	}
}
