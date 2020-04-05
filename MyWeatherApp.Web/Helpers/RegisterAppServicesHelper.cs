using Microsoft.Extensions.DependencyInjection;
using MyWeatherApp.Interfaces;
using MyWeatherApp.WeatherService;

namespace MyWeatherApp.Web.Helpers
{
    public static class RegisterAppServicesHelper
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherQueryService, WeatherQueryService>();
            return services;
        }
    }
}
