using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Basket;

namespace Linkdev.Talabat.Core.Domain.Contracts.Infrastructure
{
	public interface IPaymentService
	{
		public Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
	} 
}
