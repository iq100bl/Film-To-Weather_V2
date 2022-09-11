using DatabaseAccess.DbWorker.Handlers.AdminManager;
using DatabaseAccess.DbWorker.Handlers.City;
using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.DbWorker.Handlers.Filter;
using DatabaseAccess.DbWorker.Handlers.Genre;
using DatabaseAccess.DbWorker.Handlers.Movie;
using DatabaseAccess.DbWorker.Handlers.UserMoviesData;
using DatabaseAccess.DbWorker.Handlers.Weather;

namespace DatabaseAccess.DbWorker.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public ICityDbHandler Cities { get; }
        public IFilterDbHandler Filters { get; }
        public IMovieDbHandler Movies { get; }
        public IUserMoviesDataDbHandler UserMoviesData { get; }
        public IWeatherDbHandler Weather { get; }
        public IGenreDbHandler Genre { get; }
        public IAdminManagerDbHandler AdminManager { get; }

        public UnitOfWork(IWeatherDbHandler weatherDbHandler, IUserMoviesDataDbHandler userMoviesDataDbHandler,
            ICityDbHandler handler, IFilterDbHandler filter, IMovieDbHandler movieDbHandler, ApplicationContext context, IGenreDbHandler genre, IAdminManagerDbHandler adminDb)
        {
            Weather = weatherDbHandler;
            UserMoviesData = userMoviesDataDbHandler;
            Cities = handler;
            Filters = filter;
            Movies = movieDbHandler;
            _context = context;
            Genre = genre;
            AdminManager = adminDb;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
