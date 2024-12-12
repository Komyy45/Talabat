using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Basket
{
    public class CustomerBasketDto
    {
        public required string Id { get; set; }

        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

		public int? DeliveryMethodId { get; set; }
		public string? PaymentIntentId { get; set; }
		public decimal ShippingPrice { get; set; }
		public string? ClientSecret { get; set; }
	}
}
