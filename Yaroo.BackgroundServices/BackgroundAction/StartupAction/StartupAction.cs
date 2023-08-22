namespace Yaroo.BackgroundServices.BackgroundAction.StartupAction
{
    public abstract class StartupAction : BackgroundActionBase, IStartupAction
    {
        public override string Type => "StartupAction";

        public async Task ExecuteAsync(IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(DefaultStatuses.StartupAction.Running);
            await ExecuteOnStartup(services, stoppingToken);
            UpdateStatus(DefaultStatuses.StartupAction.Completed);
        }

        protected abstract Task ExecuteOnStartup(IServiceProvider services, CancellationToken stoppingToken);
    }
}
