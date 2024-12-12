using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Orders;
using Linkdev.Talabat.Core.Application.Mapping;
using Linkdev.Talabat.Core.Application.Services;
using Linkdev.Talabat.Core.Application.Services.Basket;
using Linkdev.Talabat.Core.Application.Services.Orders;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
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

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped<IBasketService, BasketService>();

            //services.AddScoped<Func<IBasketService>>(serviceProvider =>
            //{
            //    return () => serviceProvider.GetRequiredService<IBasketService>();
            //});

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<Func<IOrderService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });

            return services;
        }
    }
}
