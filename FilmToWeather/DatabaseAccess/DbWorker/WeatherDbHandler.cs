using DatabaseAccess.Entities;

namespace DatabaseAccess.DbWorker
{
    public class WeatherDbHandler : GenericDbHandler<WeatherModel>, IWeatherDbHandler
    {
        public WeatherDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
