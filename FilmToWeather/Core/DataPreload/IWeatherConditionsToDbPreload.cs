using Core.DataPreload;

namespace Core.PreLoad
{
    public interface IWeatherConditionsToDbPreload : IInitializer
    {
        Task InitializeDataWeatherConditionToDbAsync();
    }
}
