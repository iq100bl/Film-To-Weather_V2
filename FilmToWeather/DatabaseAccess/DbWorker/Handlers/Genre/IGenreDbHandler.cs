using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;

namespace DatabaseAccess.DbWorker.Handlers.Genre
{
    public interface IGenreDbHandler : IGenericDbHandler<GenreModel>
    {
        Task<GenreModel[]> GetAll();
    }
}
