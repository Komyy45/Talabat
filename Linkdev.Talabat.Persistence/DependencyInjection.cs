using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Data;
using Linkdev.Talabat.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.Talabat.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
            migrationOptions => migrationOptions.MigrationsAssembly(typeof(AssemblyInformation).Assembly.FullName)
            ));

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

            return services;
        }
    }
}
