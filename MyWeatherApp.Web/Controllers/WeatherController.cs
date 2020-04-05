using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeatherApp.Entities.Weather;
using MyWeatherApp.Interfaces;
using System.Threading.Tasks;

namespace MyWeatherApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherQueryService _weatherQueryService;

        public WeatherController(IWeatherQueryService weatherQueryService)
        {
            _weatherQueryService = weatherQueryService;
        }

        [HttpGet("forecastByPostcode")]
        public async Task<CityWeatherInformation> ForecastByPostcode(string postCode, string countryCode)
        {
            return await _weatherQueryService.GetWeatherForecastByPostCode(postCode, countryCode);
        }

        [HttpGet("forecastByCityName")]
        public async Task<CityWeatherInformation> ForecastByCityName(string cityName, string countryCode)
        {
            return await _weatherQueryService.GetWeatherForecastByCityName(cityName, countryCode);
        }
    }
}
