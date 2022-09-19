using DatabaseAccess.Entities;

namespace WebApplicationMVC.Models
{
    public class FisitkasViewModel
    {
        public Dictionary<string, string> Fisitkas { get; set; }
        public GenreModel[] Genres { get; set; }
    }
}
