using Core.Api.ConectionService;
using Core.Api.Movie.Entities.Response;
using DatabaseAccess.Entities;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Api.Movie
{
    public class MoviesApi : IMoviesApi, IMoviesApiPreload
    {
        private readonly string _moviesApiKey;
        private readonly string _moviesBaseUrl;
        private readonly string _languageEn = "en-US";
        private readonly string _languageRu = "ru-RU";
        private readonly string _genresMovies = "genre/movie/list";
        private readonly string _recommendedMovies = "genre/movie/list";
        private readonly string _movieDetails = "movie";
        private readonly IConectionHandler _conectionHandler;

        public MoviesApi(IConfiguration configuration, IConectionHandler conectionHandler)
        {
            _moviesApiKey = configuration["MoviesApiKey"];
            _moviesBaseUrl = configuration["MoviesBaseUrl"];
            _conectionHandler = conectionHandler;
        }

        public async Task<MovieResponce> GetDetailsMovieForAnotherLang(int id, string language)
        {
            // i taking recommended movies with one lang, so i query data for second lang before save it in DB
            var info = _moviesBaseUrl
                .AppendPathSegments(_movieDetails, id)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = language == _languageEn ? _languageRu : _languageEn,
                });
            return await _conectionHandler.CallApi(() => info.GetJsonAsync<MovieResponce>());
        }

        public async Task<MoviesResponce> GetRecommendedFilms(int page, string filters, string language)
        {
            var movies = _moviesBaseUrl.AppendPathSegment(_recommendedMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language,
                    sort_by = "vote_count.desc",
                    include_adult = false,
                    include_video = false,
                    page,
                    with_genres = filters, // , это и (2C&) | это или 7C&
                    with_watch_monetization_types = "flatrate"
                });
            return await _conectionHandler.CallApi(() => movies.GetJsonAsync<MoviesResponce>());
        }

        public async Task<GenreModel[]> GetGenries()
        {
            //Me need two queries, because i working with two lang. later merging the results
            var genresEn = _moviesBaseUrl
                .AppendPathSegment(_genresMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = _languageEn,
                });
            var genresRu = _moviesBaseUrl
                .AppendPathSegment(_genresMovies)
                .SetQueryParams(new
                {
                    api_key = _moviesApiKey,
                    language = _languageRu,
                });

            var genresEnResponce = await _conectionHandler.CallApi(() => genresEn.GetJsonAsync<GenresEnResponce>());
            var genresRuResponce = await _conectionHandler.CallApi(() => genresRu.GetJsonAsync<GenresRuResponce>());
            return genresEnResponce.GenresEnResponces.Join(genresRuResponce.GenresRuResponces,
                genreEn => genreEn.Id,
                genreRu => genreRu.Id,
                (genreEn, genreRu) => new GenreModel
                {
                    Id = genreEn.Id,
                    EnName = genreEn.EnName,
                    RuName = genreRu.RuName
                }).ToArray();
        }
    }
}