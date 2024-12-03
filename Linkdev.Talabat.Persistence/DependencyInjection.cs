using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
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
            #region StoreDbContext

            services.AddScoped(typeof(AuditInterceptor));
            
            services.AddDbContext<StoreDbContext>((serviceProvider, options) =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                migrationOptions => migrationOptions.MigrationsAssembly(typeof(AssemblyInformation).Assembly.FullName))
                .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
			}
            );

            #endregion

            #region StoreIdentityDbContext
            
            services.AddDbContext<StoreIdentityDbContext>(options =>
            options
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
            migrationOptions => migrationOptions.MigrationsAssembly(typeof(AssemblyInformation).Assembly.FullName)
            ));

            services.AddScoped<IStoreIdentityDbContextIntializer, StoreIdentityDbContextIntializer>(); 

            #endregion

            services.AddScoped<IStoreDbContextInitializer, StoreDbContextInitializer>();

            return services;
        }
    }
}
