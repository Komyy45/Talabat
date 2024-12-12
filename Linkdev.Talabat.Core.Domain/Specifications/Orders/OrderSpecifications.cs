using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Linkdev.Talabat.Core.Domain.Specifications.Orders
{
	public class OrderSpecifications : BaseSpecifications<Order, int>
	{

        public OrderSpecifications(string clientEmail): base()
        {
            Criteria = O => O.BuyerEmail == clientEmail;

			AddOrderByDesc(o => o.OrderDate);
		}
		
        public OrderSpecifications(string paymentIntentId, bool isEmail): base()
        {
            Criteria = O => O.PaymentIntentId == paymentIntentId;
		}

        public OrderSpecifications(string clientEmail, int orderId) : base()
        {
			Criteria = O => O.BuyerEmail == clientEmail && O.Id == orderId;

			AddOrderByDesc(o => o.OrderDate);
		}

		protected override void AddIncludes()
		{
			Includes.Add(o => o.DeliveryMethod!);
			Includes.Add(o => o.Items);
		}
		

	}
}
