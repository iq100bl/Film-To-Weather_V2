using Core.Api.Movie.Entities.Response;
using DatabaseAccess.Entities;

namespace Core.Api.Movie
{
    public interface IMoviesApi
    {
        Task<MoviesResponce> GetRecommendedFilms(int page, string filters, string language);
        Task<MovieResponce> GetDetailsMovieForAnotherLang(int id, string language);
    }
}