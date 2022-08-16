using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class ConditionForPreloadResponce
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }
        [JsonProperty("night")]
        public string Night { get; set; }
    }
}
