using Newtonsoft.Json;

namespace Core.Api.Movie.Entities.Response
{
    internal class GenresRuResponce
    {
        [JsonProperty("genres")]
        public GenreRuResponce[] GenresRuResponces { get; set; }
    }
}
