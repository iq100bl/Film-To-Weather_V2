using Newtonsoft.Json;

namespace Core.Api.Movie.Entities.Response
{
    internal class GenresEnResponce
    {
        [JsonProperty("genres")]
        public GenreEnResponce[] GenresEnResponces { get; set; }
    }
}
