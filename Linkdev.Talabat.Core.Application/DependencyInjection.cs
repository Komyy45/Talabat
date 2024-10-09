using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;
using Linkdev.Talabat.Core.Application.Mapping;
using Linkdev.Talabat.Core.Application.Services;
using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

            services.AddScoped(typeof(IProductService), typeof(ProductService));

            return services;
        }
    }
}
