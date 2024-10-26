using Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers;
using Linkdev.Talabat.Persistence.Data;

namespace Linkdev.Talabat.Persistence._Common
{
    public abstract class DbIntializer(DbContext dbContext) : IDbIntializer
    {
        public async Task InitalizeAsync()
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
