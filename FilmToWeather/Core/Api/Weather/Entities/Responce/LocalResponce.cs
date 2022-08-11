using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class LocalResponce
    {
        [JsonProperty("name")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


    }
}
