using Core.Api.Weather.Entities.Responce;

namespace Core.Api.Weather
{
    public interface IWeatherApi
    {
        Task<WeatherApiBodyResponce> GetCurrentWeather(string city);
        Task<MoonPhaseResponce> GetMoonPhase(string city);
    }
}
