using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyWeatherApp.Entities;
using MyWeatherApp.Entities.Weather;
using MyWeatherApp.Interfaces;
using MyWeatherApp.WeatherService.Deserialization;

namespace MyWeatherApp.WeatherService
{
    public class WeatherQueryService : IWeatherQueryService
    {  
        private const string WeatherApiKeyQuerySuffix = "&APPID={0}";      
        private const string WeatherByCityNameEndpoint = "http://api.openweathermap.org/data/2.5/forecast?q={0},{1}&units=metric"; //q={city name},{country code}
        private const string WeatherByPostCodeEndpoint = "http://api.openweathermap.org/data/2.5/forecast?zip={0},{1}&units=metric"; //zip={zip code},{country code}

        private readonly string _weatherApiKey;

        private readonly ILogger _logger;
        private readonly WeatherQueryServiceDeserializer _deserializer;
        private readonly IHttpClientFactory _clientFactory;

        public WeatherQueryService(ILogger<WeatherQueryService> logger, IOptions<AppSettings> options, IHttpClientFactory clientFactory)
        {
            _weatherApiKey = options.Value.WeatherApiKey;
            _logger = logger;
            _clientFactory = clientFactory;
            _deserializer = new WeatherQueryServiceDeserializer();            
        }

        public async Task<CityWeatherInformation> GetWeatherForecastByPostCode(string postCode, string countryCode)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                throw new ArgumentNullException("postCode");
            }
            if (string.IsNullOrEmpty(countryCode))
            {
                throw new ArgumentNullException("countryCode");
            }

            return await GetWeatherInformation(string.Format(WeatherByPostCodeEndpoint, postCode, countryCode), _weatherApiKey);
        }

        public async Task<CityWeatherInformation> GetWeatherForecastByCityName(string cityName, string countryCode)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                throw new ArgumentNullException("cityName");
            }
            if (string.IsNullOrEmpty(countryCode))
            {
                throw new ArgumentNullException("countryCode");
            }

            return await GetWeatherInformation(string.Format(WeatherByCityNameEndpoint, cityName, countryCode), _weatherApiKey);
        }

        private async Task<CityWeatherInformation> GetWeatherInformation(string baseApiUrl, string apiKey)
        {
            var apiUrl = baseApiUrl + string.Format(WeatherApiKeyQuerySuffix, apiKey);
            using (var client = _clientFactory.CreateClient())
            {                    
                using (HttpResponseMessage res = await client.GetAsync(apiUrl))
                {
                    using (HttpContent content = res.Content)
                    {
                        _logger.LogInformation($"Quering cities from: {apiUrl}");
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return _deserializer.DeserializeWeatherInformation(data);
                        }
                    }
                }
            }
            _logger.LogWarning($"No queried data for: {apiUrl}");
            return new CityWeatherInformation
            {
                Name = null,
                StatusCode = 404,
                ErrorMessage = "No city found",
                WeatherByDay = new List<DailyWeather> 
                { 
                    new DailyWeather(),
                    new DailyWeather(),
                    new DailyWeather(),
                    new DailyWeather(),
                    new DailyWeather(),
                    new DailyWeather()
                }
            };           
        }
    }
}
