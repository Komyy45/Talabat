using AutoMapper;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Products;
using Linkdev.Talabat.Dashboard.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.Dashboard.Controllers.Products
{
	[Authorize(AuthenticationSchemes = "Identity.Application")]
	public class ProductsController(IUnitOfWork unitOfWork, IAttachmentService attachmentService, IMapper mapper) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var products = await unitOfWork.GetRepository<Product,int>().GetAllAsync();
			var mappedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products);
			return View(mappedProducts);
		}

		public async Task<IActionResult> Details(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
			var mappedProduct = mapper.Map<ProductDetailsViewModel>(product);
			return View(mappedProduct);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Create(ProductCreateEditViewModel product)
		{
			if (!ModelState.IsValid) return View(product);

            var mappedProduct = mapper.Map<Product>(product);

			if(product.Picture is not null)
			{
				mappedProduct.PictureUrl = await attachmentService.UploadFile(product.Picture, "images");
				if (mappedProduct.PictureUrl is null)
				{
					ModelState.AddModelError(string.Empty, "This Image is not valid");
					return View(product);
				}
			}
			mappedProduct.NormalizedName = product.Name.ToUpper();
			mappedProduct.CreatedBy = "1";
			mappedProduct.LastModifiedBy = "1";
			mappedProduct.CreatedOn = DateTime.UtcNow;
			mappedProduct.LastModifiedOn = DateTime.UtcNow;

			await unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);

			await unitOfWork.CompleteAsync();
			
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
			return View(mapper.Map<ProductCreateEditViewModel>(product));
		}

		[HttpPost]
        public async Task<IActionResult> Edit(ProductCreateEditViewModel product)
		{
			if (!ModelState.IsValid) return View(product);


            var mappedProduct = mapper.Map<Product>(product);

            if (product.Picture is not null)
            {
                mappedProduct.PictureUrl = await attachmentService.UploadFile(product.Picture, "images");
                if (mappedProduct.PictureUrl is null)
                {
                    ModelState.AddModelError(string.Empty, "This Image is not valid");
                    return View(product);
                }
            }
			else mappedProduct.PictureUrl = product.PictureUrl;
            mappedProduct.NormalizedName = product.Name.ToUpper();
            mappedProduct.CreatedBy = "1";
            mappedProduct.LastModifiedBy = "1";
            mappedProduct.CreatedOn = DateTime.UtcNow;
            mappedProduct.LastModifiedOn = DateTime.UtcNow;
            unitOfWork.GetRepository<Product, int>().Update(mappedProduct);

		

            await unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
		}

		[HttpGet]
        public async Task<IActionResult> Delete(int id)
		{
			var product = await unitOfWork.GetRepository<Product,int>().GetAsync(id);

			if (product is null) return NotFound(); 

			unitOfWork.GetRepository<Product, int>().Delete(product);
            await unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
		}
    }
}
