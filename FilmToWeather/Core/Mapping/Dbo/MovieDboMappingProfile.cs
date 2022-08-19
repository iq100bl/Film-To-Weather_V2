using AutoMapper;
using Core.Api.Movie.Entities.Response;
using Core.Data.DboEntityes;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Dbo
{
    public class MovieDboMappingProfile : Profile
    {
        public MovieDboMappingProfile()
        {
            CreateMap<MovieResponce, FilmDbo>().ForMember(dest => dest.Lang, opt => opt.Ignore());
            CreateMap<MovieModel, FilmDbo>().ForMember(dest => dest.Lang, opt => opt.Ignore());
            CreateMap<FilmDbo, MovieModel>().ForMember(dest => dest.UserMovieDatas, opt => opt.Ignore());
        }
    }
}
