	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Orders;

namespace Linkdev.Talabat.Persistence.Data.Config.Orders
{
	internal class DeliveryMethodConfigurations : BaseEntityConfigurations<DeliveryMethod, int>
	{
		public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			base.Configure(builder);

			builder.Property(DM => DM.Cost)
				   .HasColumnType("decimal(8,2)");
		}
	}
}
