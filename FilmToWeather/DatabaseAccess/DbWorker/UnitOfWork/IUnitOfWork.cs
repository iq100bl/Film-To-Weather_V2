using DatabaseAccess.DbWorker.Handlers;
using DatabaseAccess.DbWorker.Repositories;
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

        Task Save();
    }
}
