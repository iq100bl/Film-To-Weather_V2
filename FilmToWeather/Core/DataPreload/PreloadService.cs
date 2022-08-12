namespace Core.DataPreload
{
    public class PreloadService : IPreloadInitService, IPreloadSubscribeService
    {
        Func<Task> preloadMetods;

        public void Subscribe<TInitializer>(Func<Task> initHandler) where TInitializer : IInitializer
        {
            if (preloadMetods == null)
            {
                preloadMetods = initHandler;
            }
            else
            {
                preloadMetods += initHandler;
            }
        }

        public Task Init()
        {
            preloadMetods?.Invoke();

            return Task.CompletedTask;
        }
    }
}
