using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Payment;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Payment
{

	public class PaymentController(IPaymentService paymentService) : BaseApiController
	{
		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var customerBasket = await paymentService.CreateOrUpdatePaymentIntent(basketId);	
			return Ok(customerBasket);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			await paymentService.UpdateOrderStatus(json, Request.Headers["Stripe-Signature"]!);
			
			return Ok();
		}
	}
}
