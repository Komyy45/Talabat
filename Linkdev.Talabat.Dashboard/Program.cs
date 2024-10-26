using Linkdev.Talabat.APIs;
using Linkdev.Talabat.APIs.Extensions;
using Linkdev.Talabat.Core.Application;
using Linkdev.Talabat.Infrastructure;
using Linkdev.Talabat.Persistence;

namespace Linkdev.Talabat.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            
            builder.Services.AddControllersWithViews(); 
            builder.Services.AddDashboardServices();
			builder.Services.AddPresentationServices();
			builder.Services.AddApplicationServices();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            

			#endregion

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Kestrel Middlewares
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 

            #endregion

            app.Run();
        }
    }
}
