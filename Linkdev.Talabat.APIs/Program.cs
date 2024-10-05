using System.Runtime.InteropServices;
using Linkdev.Talabat.Persistence;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistenceServices(builder.Configuration); 

            #endregion

            var app = builder.Build();

            #region Applying Pending Migrations
            
            var scope = app.Services.CreateAsyncScope();

            var storeContext = scope.ServiceProvider.GetService<StoreContext>();

            var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();

            try
            {
                var pendingMigrations = await storeContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                    await storeContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<ILogger<Program>>();

                logger.LogError(ex, "An Error has been Occured!");

            }
            finally
            {
                await scope.DisposeAsync();
            }

            #endregion

            // Configure the HTTP request pipeline.
            #region Configure Kestral Middlewares
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
