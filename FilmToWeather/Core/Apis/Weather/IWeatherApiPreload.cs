using Core.Api.Weather.Entities.Responce;

namespace Core.Api.Weather
{
    public interface IWeatherApiPreLoad
    {
        Task<ConditionForPreloadResponce[]> GetWeatherConditionsDoc();
    }
}
