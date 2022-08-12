using Core.Api.ConectionService;
using Core.Api.Weather.Entities.Responce;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Api.Weather
{
    internal class WeatherApi : IWeatherApi, IWeatherApiAutoLoad
    {
        private readonly IConectionHandler _conectionHandler;
        private readonly string _weatherApiKey;
        private readonly string _weatherBaseUrl;
        private readonly string _weatherDocsForAutoLoad;
        private readonly string _currentWeather = "current.json";
        private readonly string _moonPhase = "astronomy.json";
        public WeatherApi(IConfiguration configuration, IConectionHandler conectionHandler, string weatherDocsForAutoLoad)
        {
            _weatherApiKey = configuration["WeatherApiKey"];
            _weatherBaseUrl = configuration["WeatherBaseUrl"];
            _conectionHandler = conectionHandler;
            _weatherDocsForAutoLoad = weatherDocsForAutoLoad;
        }

        public async Task<WeatherApiBodyResponce> GetCurrentWeather(string city)
        {
            var currentWeather = _weatherBaseUrl.AppendPathSegment(_currentWeather).SetQueryParams(new
            {
                key = _weatherApiKey,
                q = city
            });

            return await _conectionHandler.CallApi(() => currentWeather.GetJsonAsync<WeatherApiBodyResponce>());
        }

        public async Task<MoonPhaseResponce> GetMoonPhase(string city)
        {
            var moonPhase = _weatherBaseUrl.AppendPathSegment(_moonPhase).SetQueryParams(new
            {
                key = _weatherApiKey,
                q = city
            });

            return await _conectionHandler.CallApi(() => moonPhase.GetJsonAsync<MoonPhaseResponce>());
        }
        
        public async Task<ConditionForAutoLoadResponce[]> GetWeatherConditionsDoc()
        {
            return await _conectionHandler.CallApi(() => _weatherDocsForAutoLoad.GetJsonAsync<ConditionForAutoLoadResponce[]>()); 
        }
    }
}
