using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Domain.Entities.Orders
{
	public class OrderItem : BaseAuditableEntity<int>
	{
        public required ProductOrderItem Product { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}
