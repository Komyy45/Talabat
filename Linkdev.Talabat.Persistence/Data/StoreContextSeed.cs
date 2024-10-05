using System.Text.Json;

namespace Linkdev.Talabat.Persistence.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(this StoreContext context)
        {
            if (!context.Brands.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if(brands?.Count > 0)
                {
                    await context.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Categories.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(data);

                if(categories?.Count > 0)
                {
                    await context.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(data);

                if(Products?.Count > 0)
                {
                    await context.AddRangeAsync(Products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
