using DatabaseAccess.Entities;

namespace WebApplicationMVC.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string? EnPosterPart { get; set; }
        public string? EnOverview { get; set; }
        public string? EnTitle { get; set; }
        public string? RuPosterPart { get; set; }
        public string? RuOverview { get; set; }
        public string? RuTitle { get; set; }
        public string? GenriesEn { get; set; }
        public string? GenriesRu { get; set; }
        public bool IsWathed { get; set; }
    }
}
