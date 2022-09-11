using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;

namespace DatabaseAccess.DbWorker.Handlers.Weather
{
    public interface IWeatherDbHandler : IGenericDbHandler<WeatherModel>
    {
    }
}
