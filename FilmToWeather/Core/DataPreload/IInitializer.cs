using DatabaseAccess;

namespace Core.DataPreload
{
    public interface IInitializer
    {
        Task InitializeAsync(ApplicationContext context);
    }
}
