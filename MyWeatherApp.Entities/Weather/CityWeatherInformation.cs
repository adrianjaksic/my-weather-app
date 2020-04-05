using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWeatherApp.Entities.Weather
{
    public class CityWeatherInformation : BaseEntity
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("days")]
        public IList<DailyWeather> WeatherByDay { get; set; }

        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("long")]
        public decimal Longitude { get; set; }
    }
}
