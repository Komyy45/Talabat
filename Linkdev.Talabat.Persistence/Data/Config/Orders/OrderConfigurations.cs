using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Orders;

namespace Linkdev.Talabat.Persistence.Data.Config.Orders
{
	internal class OrderConfigurations : BaseAuditableEntityConfigurations<Order, int>
	{
		public override void Configure(EntityTypeBuilder<Order> builder)
		{
			base.Configure(builder);

			builder.OwnsOne(O => O.ShippingAddress);

			builder.Property(O => O.Status)
					.HasConversion(
					(OStatus) => OStatus.ToString(),
					(OStatus) => Enum.Parse<OrderStatus>(OStatus)
				);

			builder.HasOne(O => O.DeliveryMethod)
				.WithMany()
				.HasForeignKey(O => O.DeliveryMethodId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasMany(O => O.Items)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(O => O.SubTotal)
				   .HasColumnType("decimal(8,2)");
		}
	}
}
