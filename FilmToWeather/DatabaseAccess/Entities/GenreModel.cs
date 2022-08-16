using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccess.Entities
{
    public class GenreModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public ICollection<FilmModel> Films { get; set; }
        public ICollection<MainFisitkaForProjectModel> MainFisitkasForProject { get; set; }
    }
}