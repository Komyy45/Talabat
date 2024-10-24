using System.Text.Json;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers;
using Linkdev.Talabat.Persistence._Common;

namespace Linkdev.Talabat.Persistence.Data
{
    public class StoreDbContextInitializer(StoreDbContext dbContext) : DbIntializer(dbContext), IStoreDbContextInitializer
    {
        public override async Task SeedAsync()
        {
            if (!dbContext.Brands.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if (brands?.Count > 0)
                {
                    await dbContext.AddRangeAsync(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(data);

                if (categories?.Count > 0)
                {
                    await dbContext.AddRangeAsync(categories);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(data);

                if (Products?.Count > 0)
                {
                    await dbContext.AddRangeAsync(Products);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
