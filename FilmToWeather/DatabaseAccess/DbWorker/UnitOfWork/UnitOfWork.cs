using DatabaseAccess.DbWorker.Handlers;
using DatabaseAccess.DbWorker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UnitOfWork(IWeatherDbHandler weatherDbHandler, IUserMoviesDataDbHandler userMoviesDataDbHandler,
            ICityDbHandler handler, IFilterDbHandler filter, IMovieDbHandler movieDbHandler, ApplicationContext context, IGenreDbHandler genre)
        {
            Weather = weatherDbHandler;
            UserMoviesData = userMoviesDataDbHandler;
            Cities = handler;
            Filters = filter;
            Movies = movieDbHandler;
            _context = context;
            Genre = genre;
        }

        public Task Save()
        {
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
