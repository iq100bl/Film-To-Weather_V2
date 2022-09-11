using Core.Data;
using Core.Data.DboEntityes;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class FilmToWeatherController : Controller
    {
        private readonly ITransferService _transferService;
        private static readonly List<MovieDbo> moviesDbo = new();
        public FilmToWeatherController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public async Task<IActionResult> Index()
        {
            moviesDbo.Clear();
            moviesDbo.AddRange(await _transferService.GetRecommendedMovies("en-US"));
            return View(MappingDboToViewModel(moviesDbo));
        }

        public async Task<IActionResult> SaveMovie(int movieId, bool isWathed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieForSave = moviesDbo.SingleOrDefault(x => x.Id == movieId);
            if (movieForSave == null)
            {
                return NotFound();
            }
            moviesDbo.Remove(movieForSave);
            await _transferService.SaveMovie(movieForSave!, isWathed);
            return View("Index", MappingDboToViewModel(moviesDbo));
        }

        private MovieViewModel[] MappingDboToViewModel(List<MovieDbo> movieDbos)
        {
            return movieDbos.Select(x => new MovieViewModel
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
        }
    }
}
