namespace Yaroo.BackgroundServices.BackgroundAction
{
    public interface IBackgroundAction
    {
        public string Name { get; }

        public string Type { get; }

        public string Status { get; }

        void Stop();
    }

    public interface IBackgroundAction<TIterationInput> : IBackgroundAction
    {
        Task ExecuteAsync(TIterationInput input, IServiceProvider services, CancellationToken stoppingToken);
    }
}
