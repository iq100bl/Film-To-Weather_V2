using Core.Data.DboEntityes;
using DatabaseAccess.Entities;

namespace Core.Data
{
    public interface ITransferService
    {
        Task<Guid> ValidityCheckedCityForUser(string city);
        Task<MovieDbo[]> GetRecommendedMovies(string lang);
        Task SaveMovie(MovieDbo movie, bool isWathed);
        Task<MovieDbo[]> GetAllUserMovies();
        Task ChangingWathed(MovieDbo movieDbo);
    }
}
