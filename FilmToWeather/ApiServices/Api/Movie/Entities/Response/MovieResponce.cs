using Newtonsoft.Json;

namespace ApiServices.Api.Movie.Entities.Response
{
    internal class MovieResponce
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string release_date { get; set; }

        [JsonProperty("genre_ids")]
        public ICollection<GenreEnResponce> Genres { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }


title
string
optional
backdrop_path
string or null
optional
popularity
number
optional
vote_count
integer
optional
video
boolean
optional
vote_average
number
optional
total_results
integer
optional
total_pages
integer
optional
    }
}
