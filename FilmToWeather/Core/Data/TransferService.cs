using AutoMapper;
using Core.Api.Movie;
using Core.Api.Weather;
using Core.Api.Weather.Entities.Responce;
using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class TransferService : ITransferService
    {
        private readonly IWeatherApi _weatherApi;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TransferService(ApplicationContext context, IWeatherApi weatherApi, IMapper mapper)
        {
            _context = context;
            _weatherApi = weatherApi;
            _mapper = mapper;
        }

        public async Task<Guid> ValidityCheckedCityForUser(string city)
        {
            city = string.Concat(char.ToUpper(city[0]), city[1..]);
            var cityCurrentUser = await _context.City.AsNoTracking().SingleOrDefaultAsync(x => x.City == city);
            if (cityCurrentUser != default)
            {
                return cityCurrentUser.Id;
            }
            else
            {
                var weatherForCurrentCity = await _weatherApi.GetCurrentWeather(city);
                var currentCity = _mapper.Map<CityModel>(weatherForCurrentCity);
                currentCity.Weather = _mapper.Map<WeatherModel>(weatherForCurrentCity);
                await _context.City.AddAsync(currentCity);
                await _context.SaveChangesAsync();
                return currentCity.Id;
            }
        }
    }
}
