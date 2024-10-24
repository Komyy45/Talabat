using Linkdev.Talabat.APIs.Controllers.Errors;
using Linkdev.Talabat.APIs.Extensions;
using Linkdev.Talabat.APIs.Middlewares;
using Linkdev.Talabat.Core.Application;
using Linkdev.Talabat.Infrastructure;
using Linkdev.Talabat.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            
            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => 
                { 
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        return new BadRequestObjectResult(new ApiValidationErrorResponse(400)
                        {
                            Errors = actionContext.ModelState.Where(state => state.Value?.Errors.Count > 0)
                                                                .Select(state => new { state.Key, Errors = state.Value.Errors.Select(error => error.ErrorMessage) })
                                                                .ToDictionary(error => error.Key, error => error.Errors)
                        });
                    };
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPresentationServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices();


            #endregion

            var app = builder.Build();

            #region Database Initialization

            await app.InitializeStoreContextAsync();

            #endregion

            // Configure the HTTP request pipeline.
            #region Configure Kestral Middlewares

            app.UseMiddleware<CustomExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();   

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
