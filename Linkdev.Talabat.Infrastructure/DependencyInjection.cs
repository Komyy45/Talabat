using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Infrastructure.BasketRepsitory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Linkdev.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;
        }
    }
}
