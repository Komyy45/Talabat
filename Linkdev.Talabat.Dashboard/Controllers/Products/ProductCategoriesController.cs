using AutoMapper;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Dashboard.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.Dashboard.Controllers.Products
{
	[Authorize(AuthenticationSchemes = "Identity.Application")]
	public class ProductCategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
			var mappedCategories = mapper.Map<IEnumerable<ProductCategoryViewModel>>(categories);
			return View(mappedCategories);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductCategoryViewModel model)
		{
			var mappedBrand = mapper.Map<ProductCategory>(model);
			mappedBrand.CreatedBy = mappedBrand.LastModifiedBy = "1";
			mappedBrand.CreatedOn = mappedBrand.LastModifiedOn = DateTime.UtcNow;
			await unitOfWork.GetRepository<ProductCategory, int>().AddAsync(mappedBrand);
			await unitOfWork.CompleteAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var category = await unitOfWork.GetRepository<ProductCategory, int>().GetAsync(id);

			if (category is null) return NotFound();

			unitOfWork.GetRepository<ProductCategory, int>().Delete(category);

			await unitOfWork.CompleteAsync();

			return RedirectToAction("Index");
		}
	}
}
