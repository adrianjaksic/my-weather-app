using MyWeatherApp.Entities.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MyWeatherApp.WeatherService.Deserialization
{
    public class WeatherQueryServiceDeserializer
    {
        private const string CityPropertyName = "city";
        private const string CityNamePropertyName = "name";
        private const string CoordinatesPropertyName = "coord";
        private const string LatitudeCoordinatePropertyName = "lat";
        private const string LongitudeCoordinatePropertyName = "lon";

        private const string ListPropertyName = "list";
        private const string DatePropertyName = "dt";
        private const string DateAsTextPropertyName = "dt_txt";
        private const string MainDataPropertyName = "main";
        private const string MinTemperaturePropertyName = "temp_min";
        private const string MaxTemperaturePropertyName = "temp_max";
        private const string TemperaturePropertyName = "temp";
        private const string HumidityPropertyName = "humidity";
        private const string WindPropertyName = "wind";
        private const string WindSpeedPropertyName = "speed";

        public CityWeatherInformation DeserializeWeatherInformation(string data)
        {
            try
            {
                using (var jsonDoc = JsonDocument.Parse(data))
                {
                    var root = jsonDoc.RootElement;
                    var city = GetCityInformationFromJson(root);
                    return city;
                }
            }
            catch
            {
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

        private CityWeatherInformation GetCityInformationFromJson(JsonElement root)
        {
            var city = new CityWeatherInformation();

            var cityProperty = root.GetProperty(CityPropertyName);
            city.Name = cityProperty.GetProperty(CityNamePropertyName).GetString();

            var coord = cityProperty.GetProperty(CoordinatesPropertyName);
            city.Latitude = coord.GetProperty(LatitudeCoordinatePropertyName).GetDecimal();
            city.Longitude = coord.GetProperty(LongitudeCoordinatePropertyName).GetDecimal();

            city.WeatherByDay = GetWeatherDataFromJson(city, root);
            return city;
        }

        private List<DailyWeather> GetWeatherDataFromJson(CityWeatherInformation city, JsonElement root)
        {
            var weatherData = root.GetProperty(ListPropertyName);
            var weatherInformations = new List<WeatherInfo>();
            for (var i = 0; i < weatherData.GetArrayLength(); i++)
            {
                weatherInformations.Add(GetWeatherInfo(weatherData[i]));
            }

            return weatherInformations.GroupBy(x => x.Date.Date).Select(x => new DailyWeather 
            { 
                Day = x.Key,
                AverageTemperature = Math.Round(x.Average(x => x.Temperature), 2),
                MinTemperature = x.Min(x => x.TemperatureMin),
                MaxTemeprature = x.Max(x => x.TemperatureMax),
                AverageHumidity = Math.Round(x.Average(x => x.Humidity), 2),
                AverageWindSpeed = Math.Round(x.Average(x => x.WindSpeed), 2),
            }).ToList();
        }

        private WeatherInfo GetWeatherInfo(JsonElement jsonElement)
        {
            var dateAsTextProperty = jsonElement.GetProperty(DateAsTextPropertyName);
            DateTime.TryParse(dateAsTextProperty.GetString(), out DateTime date);

            var mainProperty = jsonElement.GetProperty(MainDataPropertyName);
            var dataTempMin = mainProperty.GetProperty(MinTemperaturePropertyName).GetDecimal();

            var dataTempMax = mainProperty.GetProperty(MaxTemperaturePropertyName).GetDecimal();

            var temperature = mainProperty.GetProperty(TemperaturePropertyName).GetDecimal();
            var humidity = mainProperty.GetProperty(HumidityPropertyName).GetDecimal();

            var windInfo = jsonElement.GetProperty(WindPropertyName);
            var windSpeed = windInfo.GetProperty(WindSpeedPropertyName).GetDecimal();

            return new WeatherInfo
            {
                Date = date,
                Temperature = temperature,
                TemperatureMin = dataTempMin,
                TemperatureMax = dataTempMax,
                Humidity = humidity,
                WindSpeed = windSpeed,
            };
        }
    }
}
