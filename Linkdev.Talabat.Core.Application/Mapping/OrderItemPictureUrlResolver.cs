using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Orders;
using Linkdev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.Extensions.Configuration;

namespace Linkdev.Talabat.Core.Application.Mapping
{
	internal class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
	{
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.PictureUrl))
				return $"{configuration["Urls:ApiBaseUrl"]}{source.Product.PictureUrl}";

			return string.Empty;
		}
	}
}
