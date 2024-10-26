using System.ComponentModel.DataAnnotations;

namespace Linkdev.Talabat.Dashboard.Models.Products
{
	public class ProductCreateEditViewModel
	{
        public int Id { get; set; }

        [Required]
		public required string Name { get; set; }

		[Required]
		public required string Description { get; set; }

		public IFormFile? Picture { get; set; }
		public string? PictureUrl { get; set; }

		[Required]
		public decimal Price { get; set; }

		public int? BrandId { get; set; }

		public int? CategoryId { get; set; } 
	}
}
