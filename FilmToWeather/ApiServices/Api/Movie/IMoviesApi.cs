using ApiServices.Api.Movie.Entities.Response;
using DatabaseAccess.Entities;

namespace ApiServices.Api.Movie
{
    public interface IMoviesApi
    {
        Task<GenreModel[]> GetGenries();
        Task<MoviesResponce> GetRecommendedFilms(int page, string filters, string language);
        Task<MovieResponce> GetDetailsMovieForAnotherLang(int id, string language);
    }
}