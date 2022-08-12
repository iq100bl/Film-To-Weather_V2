using AutoMapper;
using Core.Api.Weather;
using Core.DataPreload;
using DatabaseAccess;
using DatabaseAccess.Entities;

namespace Core.PreLoad
{
    internal class WeatherConditionsToDbPreload : IWeatherConditionsToDbPreload
    {
        private readonly IWeatherApiAutoLoad _weatherApiAutoLoad;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public WeatherConditionsToDbPreload(IWeatherApiAutoLoad weatherApiAutoLoad, IMapper mapper, ApplicationContext context, IPreloadSubscribeService preloadService)
        {
            _weatherApiAutoLoad = weatherApiAutoLoad;
            _mapper = mapper;
            _context = context;

            preloadService.Subscribe<IWeatherConditionsToDbPreload>(InitializeDataWeatherConditionToDbAsync);
        }

        public async Task InitializeDataWeatherConditionToDbAsync()
        {
            try
            {
                var conditions = _mapper.Map<ConditionModel[]>(await _weatherApiAutoLoad.GetWeatherConditionsDoc());
                await _context.AddRangeAsync(conditions);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Falled preload data");
            }
        }

    }
}
