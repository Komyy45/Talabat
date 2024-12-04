using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;

namespace Linkdev.Talabat.Core.Domain.Contracts.Infrastructure
{
	public interface IPaymentService
	{
		public Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId);
	} 
}
