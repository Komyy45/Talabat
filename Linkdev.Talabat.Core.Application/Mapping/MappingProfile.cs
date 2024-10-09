using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Products;
using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Products

                CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, memberOptions => memberOptions.MapFrom(src => src.Brand!.Name))
                .ForMember(D => D.Category, memberOptions => memberOptions.MapFrom(src => src.Category!.Name));
                
                CreateMap<ProductBrand, BrandDto>();
                
                CreateMap<ProductCategory, CategoryDto>(); 

            #endregion
        }
    }
}
