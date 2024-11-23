using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Data.Config.Orders
{
	[DbContext(typeof(StoreDbContext))]
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
