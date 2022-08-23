using Core.Api.Movie.Entities.Response;
using DatabaseAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DboEntityes
{
    public class MovieDbo
    {
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string OriginalTitle { get; set; }
        public string EnPosterPath { get; set; }
        public string EnOverview { get; set; }
        public string EnTitle { get; set; }
        public string RuPosterPath { get; set; }
        public string RuOverview { get; set; }
        public string RuTitle { get; set; }
        public ICollection<GenreModel> Genries { get; set; }
        public bool IsWathed { get; set;}
        public string Lang { get; set; }
    }
}
