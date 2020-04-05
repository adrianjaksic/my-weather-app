using System;

namespace MyWeatherApp.Entities.Weather
{
    public class DailyWeather
    {
        public DateTime Day { get; set; }
        public decimal AverageTemperature { get; set; }
        public decimal MinTemperature { get; set; }
        public decimal MaxTemeprature { get; set; }
        public decimal AverageHumidity { get; set; }
        public decimal AverageWindSpeed { get; set; }
    }
}
