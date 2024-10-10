using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Persistence.Data;

namespace Linkdev.Talabat.APIs.Extensions
{
    public static class IntializerExtenstions
    {
        public static async Task InitializeStoreContextAsync(this WebApplication app)
        {
            var scope = app.Services.CreateAsyncScope();

            var storeContextInitializer = scope.ServiceProvider.GetService<IStoreContextInitializer>();

            var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();

            try
            {
                await storeContextInitializer!.InitalizeAsync();

                await storeContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {

                var logger = loggerFactory!.CreateLogger<ILogger<Program>>();

                logger.LogError(ex, "An Error has been Occured!");

            }
            finally
            {
                await scope.DisposeAsync();
            }
        }
    }
}
