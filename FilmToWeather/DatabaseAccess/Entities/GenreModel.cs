using DatabaseAccess.Entities.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccess.Entities
{
    public class GenreModel : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public ICollection<MovieModel> Films { get; set; }
        public ICollection<MainFisitkaForProjectModel> MainFisitkasForProject { get; set; }
    }
}