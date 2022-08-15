using Newtonsoft.Json;

namespace Core.Api.Movie.Entities.Response
{
    public class GenreEnResponce
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string EnName { get; set; }
    }
}
