namespace Yaroo.BackgroundServices.BackgroundAction.StartupAction
{
    public interface IStartupAction : IBackgroundAction
    {
        Task ExecuteAsync(IServiceProvider services, CancellationToken stoppingToken);
    }
}
