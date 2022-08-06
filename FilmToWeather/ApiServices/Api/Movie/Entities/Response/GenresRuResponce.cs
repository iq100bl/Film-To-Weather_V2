using Newtonsoft.Json;

namespace ApiServices.Api.Movie.Entities.Response
{
    internal class GenresRuResponce
    {
        [JsonProperty("genres")]
        public GenreRuResponce[] GenresRuResponces { get; set; }
    }
}
