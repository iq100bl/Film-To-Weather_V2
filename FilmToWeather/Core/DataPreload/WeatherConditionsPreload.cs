using AutoMapper;
using Core.Api.Weather;
using Core.DataPreload;
using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.PreLoad
{
    public class WeatherConditionsPreload : IInitializer
    {
        private readonly IWeatherApiPreLoad _weatherApiPreload;
        private readonly IMapper _mapper;

        public WeatherConditionsPreload(IWeatherApiPreLoad weatherApiAutoLoad,
            IMapper mapper)
        {
            _weatherApiPreload = weatherApiAutoLoad;
            _mapper = mapper;
        }

        public async Task InitializeAsync(ApplicationContext context)
        {
            context.Database.OpenConnection();
            try
            {
                var conditions = _mapper.Map<ConditionModel[]>(await _weatherApiPreload.GetWeatherConditionsDoc());
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Condition ON");
                if (context.Condition.Any())
                {
                    context.Condition.UpdateRange(conditions);
                }
                else
                {
                    context.Condition.AddRange(conditions);
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Condition OFF");
            }
            catch
            {
                throw new InvalidOperationException("Falled preload data");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

    }
}
