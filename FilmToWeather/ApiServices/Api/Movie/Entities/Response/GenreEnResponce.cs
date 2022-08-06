using Newtonsoft.Json;

namespace ApiServices.Api.Movie.Entities.Response
{
    internal class GenreEnResponce
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string EnName { get; set; }
    }
}
