using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class CurrentWeatherResponce
    {
        [JsonProperty("temp_c")]
        public int Temperature { get; set; }

        [JsonProperty("is_day")]
        public int IsDay { get; set; }

        [JsonProperty("condition")]
        public ConditionResponce Condition { get; set; }
    }
}
