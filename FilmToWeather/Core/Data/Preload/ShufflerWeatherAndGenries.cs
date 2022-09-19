using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace Core.DataPreload
{
    public class ShufflerWeatherAndGenries : IInitializer
    {
        private System.Timers.Timer aTimer;
        private ApplicationContext _context;
        public Task InitializeAsync(ApplicationContext context)
        {
            if (!context.Fisitkas.AsNoTracking().Any())
            {
                _context = context;
                SetTimer();
            }

            return Task.CompletedTask;
        }

        private async void AssociateConditionAndWeather(Object? obj, ElapsedEventArgs e)
        {
            if (_context != null)
            {
                if (_context.Condition.Any() && _context.Genres.Any())
                {
                    aTimer.Stop();
                    aTimer.Dispose();

                    List<MainFisitkaForProjectModel> filtersMovies = new();
                    var genries = _context.Genres.ToList();
                    foreach (var condition in _context.Condition)
                    {
                        var genre = genries[new Random().Next(0, genries.Count)];
                        filtersMovies.Add(new MainFisitkaForProjectModel
                        {
                            ConditionCode = condition.Code,
                            GenreId = genre.Id,
                        });
                    }

                    _context.AddRange(filtersMovies);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                aTimer.Stop();
                aTimer.Dispose();
            }
        }
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(5000);

            aTimer.Elapsed += AssociateConditionAndWeather;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}
