using MyWeatherApp.Entities.Weather;
using System.Threading.Tasks;

namespace MyWeatherApp.Interfaces
{ 
    public interface IWeatherQueryService
    {
        public Task<CityWeatherInformation> GetWeatherForecastByPostCode(string postCode, string countryCode);
        public Task<CityWeatherInformation> GetWeatherForecastByCityName(string cityName, string countryCode);
    }
}
