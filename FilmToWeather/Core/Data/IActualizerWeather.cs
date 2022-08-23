using DatabaseAccess.Entities;

namespace Core.Data
{
    public interface IActualizerWeather
    {
        Task<WeatherModel> ActualizeWeather(string userId);
    }
}
