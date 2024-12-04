using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Payment
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class PaymentController(IPaymentService paymentService) : BaseApiController
	{
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var customerBasket = await paymentService.CreateOrUpdatePaymentIntent(basketId);	
			return Ok(customerBasket);
		}
	}
}
