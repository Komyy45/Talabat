using Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers;
using Linkdev.Talabat.Persistence.Data;
using Linkdev.Talabat.Persistence.Data.Interceptors;
using Linkdev.Talabat.Persistence.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.Talabat.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            options
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
            migrationOptions => migrationOptions.MigrationsAssembly(typeof(AssemblyInformation).Assembly.FullName)
            ));

            services.AddDbContext<StoreIdentityDbContext>(options =>
            options
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("IdentityConnection"), 
            migrationOptions => migrationOptions.MigrationsAssembly(typeof(AssemblyInformation).Assembly.FullName)
            ));

            services.AddScoped<IStoreIdentityDbContextIntializer, StoreIdentityDbContextIntializer>();
            services.AddScoped<IStoreDbContextInitializer, StoreDbContextInitializer>();

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

            return services;
        }
    }
}
