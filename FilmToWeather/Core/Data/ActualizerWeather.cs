using AutoMapper;
using Core.Api.Weather;
using DatabaseAccess.DbWorker.Repositories;
using DatabaseAccess.DbWorker.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public ActualizerWeather(IMapper maper, IWeatherApi weatherApi, IUnitOfWork unitOfWork)
        {
            _mapper = maper;
            _weatherApi = weatherApi;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeatherModel> ActualizeWeather(string userId)
        {
            var weather = await _unitOfWork.Weather.GetOne(x => x.City.Users.Select(x => x.Id).First() == userId, x => x.City);
            if (weather != null)
            {
                if (weather.TimeUpdate.Day != DateTime.UtcNow.Day)
                {
                    weather = _mapper.Map<WeatherModel>(await _weatherApi.GetCurrentWeather(weather.City.City));
                    await _unitOfWork.Weather.Update(weather);
                }
            }
            else
            {
                var city = await _unitOfWork.Cities.Find(x => x.Users.Select(x => x.Id).ToString() == userId);
                weather = _mapper.Map<WeatherModel>(await _weatherApi.GetCurrentWeather(city.City));
                await _unitOfWork.Weather.Create(weather);
            }

            await _unitOfWork.Save();
            return weather;
        }
    }
}
