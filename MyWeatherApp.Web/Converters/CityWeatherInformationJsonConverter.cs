using MyWeatherApp.Entities.Weather;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyWeatherApp.Web.Converters
{
    public class CityWeatherInformationJsonConverter : JsonConverter<CityWeatherInformation>
    {
        public override CityWeatherInformation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("This class should not be read from JSon.");
        }

        public override void Write(Utf8JsonWriter writer,
                                   CityWeatherInformation value,
                                   JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteStartObject();
                writer.WriteNumber("statusCode", value.StatusCode);
                writer.WriteString("errorMessage", value.ErrorMessage);
                writer.WriteString("name", value.Name);
                writer.WriteNumber("lat", value.Latitude);
                writer.WriteNumber("long", value.Longitude);
                if (value.WeatherByDay != null)
                {
                    writer.WriteStartArray("days");
                    foreach (var item in value.WeatherByDay)
                    {
                        writer.WriteStartObject();
                        writer.WriteString("day", item.Day.ToString("yyyy-MM-dd"));
                        writer.WriteNumber("temp", item.AverageTemperature);
                        writer.WriteNumber("minTemp", item.MinTemperature);
                        writer.WriteNumber("maxTemp", item.MaxTemeprature);
                        writer.WriteNumber("humidity", item.AverageHumidity);
                        writer.WriteNumber("windSpeed", item.AverageWindSpeed);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
            }
        }
    }
}
