using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Mapping;
using Linkdev.Talabat.Core.Application.Services;
using Linkdev.Talabat.Core.Application.Services.Basket;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped<Func<IBasketService>>(serviceProvider =>
            {

                var basketRepo = serviceProvider.GetRequiredService<IBasketRepository>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                return () => new BasketService(basketRepo, mapper, configuration);
            });

            return services;
        }
    }
}
