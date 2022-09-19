using AutoMapper;
using Core.Api.Weather;
using DatabaseAccess.DbWorker.UnitOfWork;
using DatabaseAccess.Entities;

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
            var weather = await _unitOfWork.Weather.FindOne(x => x.City.Users.Select(x => x.Id).SingleOrDefault() == userId, x => x.Condition, x => x.City);
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
                var city = await _unitOfWork.Cities.GetOne(x => x.Users.Select(y => y.Id).Single() == userId);
                weather = _mapper.Map<WeatherModel>(await _weatherApi.GetCurrentWeather(city!.City));
                await _unitOfWork.Weather.Create(weather);
            }

            return weather;
        }
    }
}
