using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Dashboard.Models.Products
{
	public class ProductDetailsViewModel
	{
        public int Id { get; set; }
		public required string CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public required string LastModifiedBy { get; set; }

		public DateTime LastModifiedOn { get; set; }

		public required string Name { get; set; }

		public required string NormalizedName { get; set; }

		public required string Description { get; set; }

		public string? PictureUrl { get; set; }

		public decimal Price { get; set; }

		public int? BrandId { get; set; } // Foriegn Key ---> ProuctBrand Entity

		public virtual ProductBrand? Brand { get; set; }

		public int? CategoryId { get; set; } // Foriegn Key ---> ProductCategory Entity

		public virtual ProductCategory? Category { get; set; }
	}
}
