using Core.Data;
using Core.Data.DboEntityes;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class UserRoomController : Controller
    {
        private readonly ITransferService _transferService;

        private static readonly List<MovieDbo> moviesDbo = new();

        public UserRoomController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public  IActionResult Index()
        {
            moviesDbo.Clear();
            moviesDbo.AddRange(_transferService.GetAllUserMovies().Result.ToList());
            SortMovie();
            return View(MappingDboToViewModel(moviesDbo));
        }
        public async Task<IActionResult> Change(int movieId)
        {
            await _transferService.ChangingWathed(moviesDbo.Single(x => x.Id == movieId));
            moviesDbo.Single(x => x.Id == movieId).IsWathed = !moviesDbo.Single(x => x.Id == movieId).IsWathed;
            SortMovie();
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

        private static void SortMovie()
        {
            var mc = new MovieCompare();
            moviesDbo.Sort(mc);
        }

        sealed class MovieCompare : IComparer<MovieDbo>
        {
            public int Compare(MovieDbo? x, MovieDbo? y)
            {
                if (x!.IsWathed)
                {
                    if (y!.IsWathed)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (y!.IsWathed)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
