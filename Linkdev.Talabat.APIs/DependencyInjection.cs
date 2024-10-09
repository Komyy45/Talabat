using Linkdev.Talabat.APIs.Services;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;

namespace Linkdev.Talabat.APIs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

            return services;
        }
    }
}
