namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Products
{
    public class ProductToReturnDto
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public string? PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; } // Foriegn Key ---> ProuctBrand Entity

        public string? Brand { get; set; }

        public int? CategoryId { get; set; } // Foriegn Key ---> ProductCategory Entity

        public string? Category { get; set; }
    }
}
