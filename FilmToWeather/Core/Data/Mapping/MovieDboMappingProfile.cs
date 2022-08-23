using AutoMapper;
using Core.Api.Movie.Entities.Response;
using Core.Data.DboEntityes;
using DatabaseAccess.Entities;

namespace Core.Mapping
{
    public class MovieDboMappingProfile : Profile
    {
        public MovieDboMappingProfile()
        {
            CreateMap<MovieResponce, MovieDbo>().ForMember(dest => dest.Lang, opt => opt.MapFrom(x => x.Language))
                .AfterMap((opt, dest) => dest.IsWathed = false)
                .AfterMap((opt, dest) => dest.Genries = new List<GenreModel>())
                .AfterMap((opt, dest) => opt.GenresId.ToList().ForEach(x => dest.Genries.Add(new GenreModel { Id = x})))
                .ForMember(dest => dest.RuPosterPath, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? null : opt.PosterPath))
                .ForMember(dest => dest.EnPosterPath, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? opt.PosterPath : null))
                .ForMember(dest => dest.RuOverview, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? null : opt.Overview))
                .ForMember(dest => dest.EnOverview, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? opt.Overview : null))
                .ForMember(dest => dest.RuTitle, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? null : opt.Title))
                .ForMember(dest => dest.EnTitle, opt => opt.MapFrom((opt, dest) => opt.Language == "en-US" ? opt.Title : null));

            CreateMap<MovieModel, MovieDbo>().ForMember(dest => dest.Lang, opt => opt.Ignore());

            CreateMap<MovieDbo, MovieModel>().ForMember(dest => dest.UserMovieDatas, opt => opt.Ignore());
        }
    }
}
