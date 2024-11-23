using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models._Common;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Orders
{
	public class CreatedOrderDto
	{
        public required int BasketId { get; set; }

        public int DeliveryMethodId { get; set; }

        public required AddressDto Address { get; set; }
    }
}
