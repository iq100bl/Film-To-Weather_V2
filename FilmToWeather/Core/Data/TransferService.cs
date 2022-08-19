using AutoMapper;
using Core.Api.Movie;
using Core.Api.Weather;
using Core.Api.Weather.Entities.Responce;
using Core.Data.DboEntityes;
using DatabaseAccess;
using DatabaseAccess.DbWorker;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class TransferService : ITransferService
    {
        private readonly IWeatherApi _weatherApi;
        private readonly IMapper _mapper;
        private readonly IMoviesApi _moviesApi;
        private readonly IMovieDbHandler _moviesDbHandler;
        private readonly ICityDbHandler _cityDbHandler;
        private readonly IActualizerWeather _actualizerWeather;
        private readonly IFilterDbHandler _filterDbHandler;
        private readonly IUserMoviesDataDbHandler _userMoviesDataDbHandler;
        private readonly IHttpContextAccessor _httpContext;

        public TransferService(IWeatherApi weatherApi, IMapper mapper, IMoviesApi moviesApi,
            IMovieDbHandler moviesDbHandler, ICityDbHandler cityDbHandler, IWeatherDbHandler weatherDbHandler,
            IActualizerWeather actualizerWeather, IFilterDbHandler filterDbHandler, IHttpContextAccessor httpContext, IUserMoviesDataDbHandler userMoviesDataDbHandler)
        {
            _weatherApi = weatherApi;
            _mapper = mapper;
            _moviesApi = moviesApi;
            _moviesDbHandler = moviesDbHandler;
            _cityDbHandler = cityDbHandler;
            _actualizerWeather = actualizerWeather;
            _filterDbHandler = filterDbHandler;
            _httpContext = httpContext;
            _userMoviesDataDbHandler = userMoviesDataDbHandler;
        }

        public async Task<Guid> ValidityCheckedCityForUser(string city)
        {
            city = string.Concat(char.ToUpper(city[0]), city[1..]);
            var cityCurrentUser = await _cityDbHandler.Get(x => x.City == city);
            if (cityCurrentUser != null)
            {
                return cityCurrentUser.Id;
            }
            else
            {
                try
                {
                    var weatherForCurrentCity = await _weatherApi.GetCurrentWeather(city);
                    var currentCity = _mapper.Map<CityModel>(weatherForCurrentCity);
                    currentCity.Weather = _mapper.Map<WeatherModel>(weatherForCurrentCity);
                    await _cityDbHandler.Create(currentCity);
                    return currentCity.Id;
                }
                catch(InvalidOperationException e)
                {
                    //TODO обработать в нужный вид
                    throw new InvalidOperationException(e.Message);
                }
            }
        }

        public async Task<FilmDbo[]> GetRecomenndedFilms(CityModel city, string lang)
        {
            var userId = _httpContext.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var weather = await _actualizerWeather.ActualizeWeather(city);
            var filter = await _filterDbHandler.Get(x => x.ConditionCode == weather.CodeCondition);
            var stringFilter = filter.ToString();
            if (stringFilter != null)
            {
                var recommendedMovies = _mapper.Map<FilmDbo[]>(await _moviesApi.GetRecommendedFilms(1, stringFilter, lang));
                var currentMoviesForUser = _mapper.Map<FilmDbo[]>(await _moviesDbHandler.GetMany(x => x.UserMovieDatas.Select(x => x.UserId).ToString() == userId));
                var recommendedMoviesBeforeFilter = recommendedMovies.ExceptBy(currentMoviesForUser.Select(x => x.Id), x => x.Id).ToList();
                recommendedMoviesBeforeFilter.ForEach(x => x.Lang = lang);
                return recommendedMoviesBeforeFilter.ToArray();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(filter)} is null");
            }
        }
    }
}
