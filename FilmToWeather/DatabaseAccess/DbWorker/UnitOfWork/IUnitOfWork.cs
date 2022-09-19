using DatabaseAccess.DbWorker.Handlers.AdminManager;
using DatabaseAccess.DbWorker.Handlers.City;
using DatabaseAccess.DbWorker.Handlers.Filter;
using DatabaseAccess.DbWorker.Handlers.Fisitkas;
using DatabaseAccess.DbWorker.Handlers.Genre;
using DatabaseAccess.DbWorker.Handlers.Movie;
using DatabaseAccess.DbWorker.Handlers.UserMoviesData;
using DatabaseAccess.DbWorker.Handlers.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICityDbHandler Cities { get; }
        IFilterDbHandler Filters { get; }
        IMovieDbHandler Movies { get; }
        IUserMoviesDataDbHandler UserMoviesData{ get; }
        IWeatherDbHandler Weather { get; }
        IGenreDbHandler Genre { get; }
        IAdminManagerDbHandler AdminManager { get; }
        IFisitkasDbHandler Fisitkas { get; }
        Task Save();
    }
}
