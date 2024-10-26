using AutoMapper;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Dashboard.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.Dashboard.Controllers.Products
{
	[Authorize(AuthenticationSchemes = "Identity.Application")]
	public class ProductBrandsController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var mappedBrands = mapper.Map<IEnumerable<ProductBrandViewModel>>(brands);
			return View(mappedBrands);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductBrandViewModel model)
		{

			var mappedBrand = mapper.Map<ProductBrand>(model);
			mappedBrand.CreatedBy = mappedBrand.LastModifiedBy = "1";
			mappedBrand.CreatedOn = mappedBrand.LastModifiedOn = DateTime.UtcNow;
		 	await unitOfWork.GetRepository<ProductBrand, int>().AddAsync(mappedBrand);
			await unitOfWork.CompleteAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);

			if (brand is null) return NotFound();

            unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);

			await unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
	}
}
