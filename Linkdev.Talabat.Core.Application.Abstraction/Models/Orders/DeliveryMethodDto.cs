using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Orders
{
	public class DeliveryMethodDto
	{
		public int Id { get; set; }

		public required string ShortName { get; set; }

		public required string Description { get; set; }

		public required string DeliveryTime { get; set; }

		public required decimal Cost { get; set; }
	}
}
