using AutoMapper;
using Core.Data;
using Core.Data.DboEntityes;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
using static System.Net.WebRequestMethods;

namespace WebApplicationMVC.Controllers
{
    public class FilmToWeatherController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly List<MovieDbo> currentRecommendedMovies;
        public FilmToWeatherController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public async Task<IActionResult> Index()
        {
            //TODO добавить маппер
            var recommendedMovies = await _transferService.GetRecommendedMovies("en-US");
            currentRecommendedMovies.AddRange(await _transferService.GetRecommendedMovies("en-US"));
            var movieViewModels = recommendedMovies.Select(x => new MovieViewModel 
            {
                Id = x.Id,
                EnOverview = x.EnOverview, 
                EnPosterPart = "https://image.tmdb.org/t/p/w300" + x.EnPosterPath, 
                EnTitle = x.EnTitle, 
                GenriesEn = x.Genries.Select(x => x.EnName).ToString(), 
                GenriesRu = x.Genries.Select(x => x.RuName).ToString(), 
                OriginalTitle = x.OriginalTitle, 
                IsWathed = x.IsWathed,
                RuOverview = x.RuOverview, 
                RuPosterPart = "https://image.tmdb.org/t/p/w500" + x.RuPosterPath, 
                RuTitle = x.RuTitle 
            }).ToArray();
            return View(movieViewModels);
        }

        public async Task<ActionResult> SaveMovie(int id)
        {
            var x = currentRecommendedMovies.Single(x => x.Id == id);
            currentRecommendedMovies.Remove(x);
            return View();
        }
    }
}
