using DatabaseAccess.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccess.Entities
{
    public class MovieModel : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string OriginalTitle { get; set; }
        public string EnPosterPart { get; set; }
        public string EnOverview { get; set; }
        public string EnTitle { get; set; }
        public string RuPosterPart { get; set; }
        public string RuOverview { get; set; }
        public string RuTitle { get; set; }
        public ICollection<GenreModel> Genries { get; set; }
        public ICollection<UserMovieData> UserMovieDatas { get; set; }
    }
}
