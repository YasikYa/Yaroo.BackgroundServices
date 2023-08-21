namespace Yaroo.BackgroundServices.BackgroundAction.StartupAction
{
    public abstract class StartupAction : BackgroundActionBase, IStartupAction
    {
        public const string Running = "Default:Running";
        public const string Completed = "Default:Completed";

        public override string Type => "StartupAction";

        public async Task ExecuteAsync(IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(Running);
            await ExecuteOnStartup(services, stoppingToken);
            UpdateStatus(Completed);
        }

        protected abstract Task ExecuteOnStartup(IServiceProvider services, CancellationToken stoppingToken);
    }
}
