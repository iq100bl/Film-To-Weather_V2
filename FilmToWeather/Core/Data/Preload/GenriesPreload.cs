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
            try
            {
                var genries = await _moviesApi.GetGenries();
                if (context.Genres.Any())
                {
                    context.Genres.UpdateRange(genries);
                }
                else
                {
                    context.Genres.AddRange(genries);
                }

                context.SaveChanges();
            }
            catch
            {
                throw new InvalidOperationException("Falled preload data");
            }
        }
    }
}
