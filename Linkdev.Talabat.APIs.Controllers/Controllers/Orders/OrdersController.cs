using System;
using System.Collections.Generic;
using System.Linq;
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
		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(CreatedOrderDto createdOrder)
		{
			string userEmail = User.FindFirstValue(ClaimTypes.Email)!;

			var order = await serviceManager.OrderService.CreateOrderAsync(userEmail!, createdOrder);

			return Ok(order);
		}
	}
}
