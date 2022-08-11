using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class ConditionResponce
    {
        [JsonProperty("code")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Condition { get; set; }
    }
}
