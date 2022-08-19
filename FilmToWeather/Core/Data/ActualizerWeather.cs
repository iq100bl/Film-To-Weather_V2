using AutoMapper;
using Core.Api.Weather;
using DatabaseAccess.DbWorker;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class ActualizerWeather : IActualizerWeather
    {
        private readonly IMapper _mapper;
        private readonly IWeatherApi _weatherApi;
        private readonly IWeatherDbHandler _weatherDbHandler;

        public ActualizerWeather(IMapper maper, IWeatherApi weatherApi, IWeatherDbHandler weatherDbHandler)
        {
            _mapper = maper;
            _weatherApi = weatherApi;
            _weatherDbHandler = weatherDbHandler;
        }

        public async Task<WeatherModel> ActualizeWeather(CityModel city)
        {
            var weather = await _weatherDbHandler.Get(x => x.City.Id == city.Id);
            if (weather != null)
            {
                if (weather.TimeUpdate.Day != DateTime.UtcNow.Day)
                {
                    weather = _mapper.Map<WeatherModel>(await _weatherApi.GetCurrentWeather(city.City));
                    await _weatherDbHandler.Update(weather);
                }
            }
            else
            {
                weather = _mapper.Map<WeatherModel>(await _weatherApi.GetCurrentWeather(city.City));
                await _weatherDbHandler.Create(weather);
            }

            return weather;
        }
    }
}
