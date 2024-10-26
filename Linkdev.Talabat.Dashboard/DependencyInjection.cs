using Linkdev.Talabat.Dashboard.Mapping;

namespace Linkdev.Talabat.Dashboard
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddDashboardServices(this IServiceCollection services)
		{
			services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

			
			return services;
		}
	}
}
