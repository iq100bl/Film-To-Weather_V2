using Core.Api.Movie;
using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace Core.DataPreload
{
    public class GenriesPreload : IInitializer
    {
        private readonly IMoviesApiPreload _moviesApi;

        public GenriesPreload(IMoviesApiPreload moviesApi)
        {
            _moviesApi = moviesApi;
        }

        public async Task InitializeAsync(ApplicationContext context)
        {
            context.Database.OpenConnection();
            try
            {
                //TODO найти выход как сделать лучше
                var genries = await _moviesApi.GetGenries();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres ON");
                if (context.Genres.Any())
                {
                    context.Genres.UpdateRange(genries);
                }
                else
                {
                    context.Genres.AddRange(genries);
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres OFF");
            }
            catch
            {
                throw new InvalidOperationException("Falled preload data");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}
