using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Api.Movie.Entities.Response
{
    public class MoviesResponce
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("results")]
        public ICollection<MovieResponce> Movies { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}
