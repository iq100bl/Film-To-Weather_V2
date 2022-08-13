using AutoMapper;
using Core.Api.Weather.Entities.Responce;
using DatabaseAccess.Entities;

namespace Core.Mapping
{
    public class ConditionMappingProfile : Profile
    {
        public ConditionMappingProfile()
        {
            CreateMap<ConditionForAutoLoadResponce, ConditionModel>()
                .ForMember(dest => dest.WeatherCondition, opt => opt.Ignore());
        }
    }
}
