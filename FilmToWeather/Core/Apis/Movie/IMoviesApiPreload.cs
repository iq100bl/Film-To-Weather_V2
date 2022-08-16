using DatabaseAccess.Entities;

namespace Core.Api.Movie
{
    public interface IMoviesApiPreload
    {
        Task<GenreModel[]> GetGenries();
    }
}