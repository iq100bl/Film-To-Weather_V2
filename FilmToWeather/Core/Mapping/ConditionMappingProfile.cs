using AutoMapper;
using Core.Api.Weather.Entities.Responce;
using DatabaseAccess.Entities;

namespace Core.Mapping
{
    public class ConditionMappingProfile : Profile
    {
        public ConditionMappingProfile()
        {
            CreateMap<ConditionForPreloadResponce, ConditionModel>()
                .ForMember(dest => dest.WeatherModel, opt => opt.Ignore());
        }
    }
}
