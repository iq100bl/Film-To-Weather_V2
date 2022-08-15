using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class ConditionResponce
    {
        [JsonProperty("code")]
        public int code { get; set; }

        [JsonProperty("text")]
        public string Condition { get; set; }
    }
}
