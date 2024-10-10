using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;

namespace Linkdev.Talabat.Core.Application.Mapping
{
    public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string?>
    {
        public string Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["Urls:ApiBaseUrl"]}{source.PictureUrl}";

            return string.Empty;
        }
    }
}
