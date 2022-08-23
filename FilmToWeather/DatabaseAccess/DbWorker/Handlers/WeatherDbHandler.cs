using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;

namespace DatabaseAccess.DbWorker.Repositories
{
    public class WeatherDbHandler : GenericDbHandler<WeatherModel>, IWeatherDbHandler
    {
        public WeatherDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
