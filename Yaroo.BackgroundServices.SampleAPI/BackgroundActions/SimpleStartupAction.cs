using Yaroo.BackgroundServices.BackgroundAction.StartupAction;

namespace Yaroo.BackgroundServices.SampleAPI.BackgroundActions
{
    public sealed class SimpleStartupAction : StartupAction
    {
        private readonly ILogger<SimpleStartupAction> _logger;

        public SimpleStartupAction(ILogger<SimpleStartupAction> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteOnStartup(IServiceProvider services, CancellationToken stoppingToken)
        {
            await Task.Delay(5_000);
            _logger.LogInformation("Simplestartup action completed");
        }
    }
}
