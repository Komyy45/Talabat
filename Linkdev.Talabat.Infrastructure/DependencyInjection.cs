using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Entities.Basket;
using Linkdev.Talabat.Infrastructure.BasketRepsitory;
using Linkdev.Talabat.Infrastructure.Payment;
using Linkdev.Talabat.Infrastructure.Payment.options;
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

            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            services.Configure<RedisSettings>(options => options.timeToLiveInDays = int.Parse(configuration["RedisSettings:timeToLiveInDays"]!));
            services.Configure<StripeSettings>(options =>
            {
                options.SecretKey = configuration["StripeSettings:SecretKey"]!;
                options.WebHookSecret = configuration["StripeSettings:WebHookSecret"]!;
             });

            return services;
        }
    }
}
