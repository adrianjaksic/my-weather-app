using System;

namespace MyWeatherApp.WeatherService.Deserialization
{
    public class WeatherInfo
    {
        public DateTime Date { get; internal set; }
        public decimal Temperature { get; internal set; }
        public decimal TemperatureMin { get; internal set; }
        public decimal TemperatureMax { get; internal set; }
        public decimal Humidity { get; internal set; }
        public decimal WindSpeed { get; internal set; }
    }
}
