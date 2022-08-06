using Newtonsoft.Json;

namespace ApiServices.Api.Movie.Entities.Response
{
    internal class GenresEnResponce
    {
        [JsonProperty("genres")]
        public GenreEnResponce[] GenresEnResponces { get; set; }
    }
}
