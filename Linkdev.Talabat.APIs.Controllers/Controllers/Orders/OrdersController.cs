using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Orders
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class OrdersController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpPost] // POST: /api/Orders
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(CreatedOrderDto createdOrder)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

			var order = await serviceManager.OrderService.CreateOrderAsync(userEmail!, createdOrder);

			return Ok(order);
		}

		[HttpGet("{id}")] // GET : /api/Orders/id
		public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

			var order = await serviceManager.OrderService.GetOrderByIdAsync(userEmail!, id);

			return Ok(order);
		}

		[HttpGet] // GET : /api/Orders
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetUserOrders()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

			var orders = await serviceManager.OrderService.GetUserOrdersAsync(userEmail!);

			return Ok(orders);
		}

		[HttpGet("DeliveryMethods")] // GET: /api/Orders/DeliveryMethods
		public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
		{
			return Ok(await serviceManager.OrderService.GetDeliveryMethodsAsync());
		}
		
	}
}
