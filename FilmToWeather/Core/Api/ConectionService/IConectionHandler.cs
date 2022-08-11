namespace Core.Api.ConectionService
{
    public interface IConectionHandler
    {
        public Task<T> CallApi<T>(Func<Task<T>> func);
    }
}
