using DatabaseAccess.Entities.Abstractions;


namespace DatabaseAccess.Entities
{
    public class UserMovieData : BaseEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int MoviesId { get; set; }
        public MovieModel FilmModel { get; set; }
        public bool IsWathced { get; set; }

    }
}
