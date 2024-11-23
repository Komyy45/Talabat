using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Orders;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Orders
{
	internal interface IOrderService
	{
		Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, CreatedOrderDto createdOrder);

		Task<OrderToReturnDto> GetOrderByIdAsync(string clientEmail, int id);

		Task<IEnumerable<OrderToReturnDto>> GetUserOrdersAsync();

		Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
