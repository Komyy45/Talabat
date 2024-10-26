using AutoMapper;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Dashboard.Models.Auth;
using Linkdev.Talabat.Dashboard.Models.Products;
using Microsoft.AspNetCore.Identity;

namespace Linkdev.Talabat.Dashboard.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
			#region Products

			CreateMap<Product, ProductViewModel>()
				.ForMember(dist => dist.Brand, memberOptions => memberOptions.MapFrom(src => src.Brand != null ? src.Brand.Name : "No Brand"))
				.ForMember(dist => dist.Category, memberOptions => memberOptions.MapFrom(src => src.Category != null ? src.Category.Name : "No Category"));

			CreateMap<ProductBrand, ProductBrandViewModel>().ReverseMap();	

			CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();	

			CreateMap<ProductCreateEditViewModel, Product>().ReverseMap();

			CreateMap<Product, ProductDetailsViewModel>().ReverseMap();

			#endregion

			#region Auth

			CreateMap<IdentityRole, RoleViewModel>().ReverseMap();

			CreateMap<ApplicationUser, ApplicationUserEditViewModel > ().ReverseMap();

			#endregion
		}
    }
}
