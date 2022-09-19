using AutoMapper;
using Core.Api.Movie;
using Core.Api.Weather;
using Core.Data.DboEntityes;
using DatabaseAccess.DbWorker.UnitOfWork;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Data
{
    public class TransferService : ITransferService
    {
        private readonly IWeatherApi _weatherApi;
        private readonly IMapper _mapper;
        private readonly IMoviesApi _moviesApi;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActualizerWeather _actualizerWeather;
        private readonly IHttpContextAccessor _httpContext;

        public TransferService(IWeatherApi weatherApi, IMapper mapper, IMoviesApi moviesApi,
            IActualizerWeather actualizerWeather, IHttpContextAccessor httpContext, IUnitOfWork unitOfWork)
        {
            _weatherApi = weatherApi;
            _mapper = mapper;
            _moviesApi = moviesApi;
            _unitOfWork = unitOfWork;
            _actualizerWeather = actualizerWeather;
            _httpContext = httpContext;
        }

        public async Task<Guid> ValidityCheckedCityForUser(string city)
        {
            city = string.Concat(char.ToUpper(city[0]), city[1..]);
            var cityCurrentUser = await _unitOfWork.Cities.Find(x => x.City == city);
            if (cityCurrentUser.Id == Guid.Empty)
            {
                try
                {
                    var weatherForCurrentCity = await _weatherApi.GetCurrentWeather(city);
                    var currentCity = _mapper.Map<CityModel>(weatherForCurrentCity);
                    currentCity.Weather = _mapper.Map<WeatherModel>(weatherForCurrentCity);
                    await _unitOfWork.Cities.Create(currentCity);
                    return currentCity.Id;
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException(e.Message);
                }
            }
            else
            {
                return cityCurrentUser.Id;
            }
        }

        public async Task<MovieDbo[]> GetRecommendedMovies(string lang)
        {
            var weather = await _actualizerWeather.ActualizeWeather(GetUserId());
            var filter = await _unitOfWork.Filters.FindOne(x => x.ConditionCode == weather.CodeCondition, x => x.Genre);
            if (filter!.GenreId != default)
            {
                var recommendedMovies = _mapper.Map<MovieDbo[]>(_moviesApi.GetRecommendedFilms(1, filter.GenreId.ToString(), lang).Result.Movies);
                var moviesUserData = await _unitOfWork.Movies.FindMany(x => x.UserMovieDatas.Select(x => x.UserId).First() == GetUserId(),
                    x => x.UserMovieDatas);
                var currentMoviesForUser = _mapper.Map<MovieDbo[]>(moviesUserData);
                var recommendedMoviesAfterFilter = recommendedMovies.ExceptBy(currentMoviesForUser.Select(x => x.Id), x => x.Id).ToList();
                var genries = await _unitOfWork.Genre.GetAll();
                recommendedMoviesAfterFilter.ForEach(movie =>
                {
                    movie.Genries = genries.IntersectBy(movie.Genries.Select(genre => genre.Id), movie => movie.Id).ToArray();
                    movie.Lang = lang;
                });
                return recommendedMoviesAfterFilter.ToArray();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(filter)} is null");
            }
        }

        public async Task SaveMovie(MovieDbo movie, bool isWathed)
        {
            var detailsAboutMovie = _mapper.Map<MovieDbo>(await _moviesApi.GetDetailsMovieForAnotherLang(movie.Id, movie.Lang));
            var movieWithDetails = JoinInfoAboutMovie(movie.Lang == "En-en" ? (detailsAboutMovie, movie) : (movie, detailsAboutMovie));
            movieWithDetails.Genries = (await _unitOfWork.Genre.GetAll()).Intersect(movieWithDetails.Genries).ToList();
            var userData = new UserMovieData
            {
                UserId = GetUserId(),
                IsWathced = isWathed,
                MovieModel = movieWithDetails
            };
            await _unitOfWork.UserMoviesData.Create(userData);
            await _unitOfWork.Save();
        }

        public async Task<MovieDbo[]> GetAllUserMovies()
        {
            var userMoviesData = await _unitOfWork.UserMoviesData.GetMany(x => x.UserId == GetUserId(), x => x.MovieModel, x => x.MovieModel.Genries);
            return _mapper.Map<MovieDbo[]>(userMoviesData);
        }

        public async Task ChangingWathed(MovieDbo movieDbo)
        {
            var userMovieData = await _unitOfWork.UserMoviesData.GetOne(x => x.UserId == GetUserId() && x.MoviesId == movieDbo.Id);
            userMovieData.IsWathced = !userMovieData.IsWathced;
            await _unitOfWork.UserMoviesData.Update(userMovieData);
            await _unitOfWork.Save();
        }

        private string GetUserId()
        {
            return _httpContext.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        private MovieModel JoinInfoAboutMovie((MovieDbo, MovieDbo) value)
        {
            return _mapper.Map<MovieModel>(new MovieDbo
            {
                Adult = value.Item1.Adult,
                EnOverview = value.Item1.EnOverview,
                EnPosterPath = value.Item1.EnPosterPath,
                EnTitle = value.Item1.EnTitle,
                Genries = value.Item1.Genries,
                Id = value.Item1.Id,
                OriginalTitle = value.Item1.OriginalTitle,
                RuOverview = value.Item2.RuOverview,
                RuPosterPath = value.Item2.RuPosterPath,
                RuTitle = value.Item2.RuTitle
            });
        }
    }
}
