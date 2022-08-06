using ApiServices.Api.Movie.Entities.Response;
using DatabaseAccess.Entities;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace ApiServices.Api.Movie
{
    public interface IMoviesApi
    {
        Task<GenreModel[]> GetGenries();
    }

    public class MoviesApi : IMoviesApi
    {
        private readonly string _moviesApiKey;
        private readonly string _filmsBaseUrl;
        private readonly string _languageEn = "en-US";
        private readonly string _languageRu = "ru-RU";
        private readonly string _genresMovies = "/genre/movie/list";
        private readonly string _recommendedMovies = "/genre/movie/list";

        public MoviesApi(IConfiguration configuration)
        {
            _moviesApiKey = configuration["MoviesApiKey"];
            _filmsBaseUrl = configuration["BaseUrlMovies"];
        }

        public async Task<FilmModel[]> GetRecommendedFilms(int page, string filters, string language)
        {
            var movies = _filmsBaseUrl.AppendPathSegment(_recommendedMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = language,
                    sort_by = "vote_count.desc",
                    include_adult = false,
                    include_video = false,
                    page = page,
                    with_genres = filters, // , это и (2C&) | это или 7C&
                    with_watch_monetization_types = "flatrate"
                });
        }

        public async Task<GenreModel[]> GetGenries()
        {
            var genresEn = _filmsBaseUrl
                .AppendPathSegment(_genresMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = _languageEn,
                });
            var genresRu = _filmsBaseUrl
                .AppendPathSegment(_genresMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = _languageRu,
                });

            var arrayGenresEn = await CallApi(() => genresEn.GetJsonAsync<GenresEnResponce>());
            var arrayGenresRu = await CallApi(() => genresRu.GetJsonAsync<GenresRuResponce>());
            return arrayGenresEn.GenresEnResponces.Join(arrayGenresRu.GenresRuResponces,
                genreEn => genreEn.Id,
                genreRu => genreRu.Id,
                (genreEn, genreRu) => new GenreModel
                {
                    Id = genreEn.Id,
                    EnName = genreEn.EnName,
                    RuName = genreRu.RuName
                }).ToArray();
        }

        private static async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {
                throw new InvalidOperationException("Inquiry not available");
            }
        }
    }
}