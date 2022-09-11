using AutoMapper;
using Core.Api.Weather.Entities.Responce;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<WeatherApiBodyResponce, CityModel>()
                .AfterMap((opt, dest) => dest.Id = Guid.NewGuid())
                .ForMember(dest => dest.City, opt => opt.MapFrom(x => x.Location.City))
                .ForMember(dest => dest.Weather, opt => opt.Ignore())
                .ForMember(dest => dest.Region, opt => opt.MapFrom(x => x.Location.Region))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(x => x.Location.Country))
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<LocalResponce, CityModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Weather, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
        }
        
    }
}
