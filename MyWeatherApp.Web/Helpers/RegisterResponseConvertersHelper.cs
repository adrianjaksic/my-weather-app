using Microsoft.Extensions.DependencyInjection;
using MyWeatherApp.Web.Converters;

namespace MyWeatherApp.Web.Helpers
{
    public static class RegisterResponseConvertersHelper
    {
        public static IMvcBuilder RegisterResponseConverters(this IMvcBuilder services)
        {
            services.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new CityWeatherInformationJsonConverter()));
            return services;
        }
    }
}
