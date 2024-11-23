using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Domain.Entities.Orders
{
	public class Order : BaseAuditableEntity<int>
	{
		public required string BuyerEmail { get; set; }

		public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public required Address ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; }

		public virtual DeliveryMethod? DeliveryMethod { get; set; }

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

		public decimal Total => SubTotal + (DeliveryMethod?.Cost ?? 0);

		public string PaymentIntentId { get; set; } = "";
    }
}
