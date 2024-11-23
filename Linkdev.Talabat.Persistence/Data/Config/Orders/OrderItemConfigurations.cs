using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Orders;

namespace Linkdev.Talabat.Persistence.Data.Config.Orders
{
	internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem, int>
	{
		public override void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			base.Configure(builder);

			builder.OwnsOne(OI => OI.Product);

			builder.Property(OI => OI.Price)
				   .HasColumnType("decimal(8,2)");
		}
	}
}
