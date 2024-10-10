using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;

namespace Linkdev.Talabat.Persistence.Data
{
    public class StoreContextInitializer(StoreContext storeContext) : IStoreContextInitializer
    {
        public async Task InitalizeAsync()
        {
            var pendingMigrations = await storeContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await storeContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (!storeContext.Brands.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if (brands?.Count > 0)
                {
                    await storeContext.AddRangeAsync(brands);
                    await storeContext.SaveChangesAsync();
                }
            }

            if (!storeContext.Categories.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(data);

                if (categories?.Count > 0)
                {
                    await storeContext.AddRangeAsync(categories);
                    await storeContext.SaveChangesAsync();
                }
            }

            if (!storeContext.Products.Any())
            {

                var data = File.ReadAllText("../Linkdev.Talabat.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(data);

                if (Products?.Count > 0)
                {
                    await storeContext.AddRangeAsync(Products);
                    await storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
