using Core.Api.Weather.Entities.Responce;

namespace Core.Api.Weather
{
    public interface IWeatherApiAutoLoad
    {
        Task<ConditionForAutoLoadResponce[]> GetWeatherConditionsDoc();
    }
}
