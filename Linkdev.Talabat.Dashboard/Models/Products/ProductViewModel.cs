namespace Linkdev.Talabat.Dashboard.Models.Products
{
	public class ProductViewModel
	{
		public required int Id { get; set; }

		public required string Name { get; set; }

		public decimal Price { get; set; }

		public string? Brand { get; set; }

		public string? Category { get; set; }
	}
}
