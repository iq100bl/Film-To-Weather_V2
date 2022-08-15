using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class WeatherApiBodyResponce
    {
        [JsonProperty("location")]
        public LocalResponce Location { get; set; }

        [JsonProperty("current")]
        public CurrentWeatherResponce Weather { get; set; }
    }
}
