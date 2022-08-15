using Newtonsoft.Json;

namespace Core.Api.Weather.Entities.Responce
{
    public class MoonPhaseResponce
    {
        [JsonProperty("moon_phase")]
        public string MoonPhase { get; set; }
    }
}
