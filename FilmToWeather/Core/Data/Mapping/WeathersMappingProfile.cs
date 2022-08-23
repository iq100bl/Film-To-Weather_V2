using AutoMapper;
using Core.Api.Weather.Entities.Responce;
using DatabaseAccess.Entities;

namespace Core.Mapping
{
    public class WeathersMappingProfile : Profile
    {
        public WeathersMappingProfile()
        {
            CreateMap<WeatherApiBodyResponce, WeatherModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(x => (int)x.Weather.Temperature))
                .AfterMap((opt, dest) => dest.TimeUpdate = DateTime.UtcNow)
                .ForMember(dest => dest.IsDay, opt => opt.MapFrom(x => x.Weather.IsDay == 1))
                .ForMember(dest => dest.CodeCondition, opt => opt.MapFrom(x => x.Weather.Condition.code));
        }
    }
}
