using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yaroo.BackgroundServices.BackgroundAction.StartupAction;
using BaseBackgroundService = Microsoft.Extensions.Hosting.BackgroundService;

namespace Yaroo.BackgroundServices.BackgroundService
{
    public sealed class StartupJob : BaseBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IStartupAction> _actions;
        private readonly ILogger<StartupJob> _logger;

        public StartupJob(IServiceProvider serviceProvider, IEnumerable<IStartupAction> actions, ILogger<StartupJob> logger)
        {
            _serviceProvider = serviceProvider;
            _actions = actions;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting startup job");
            await RunJob(stoppingToken);
            _logger.LogInformation("All startup action completed");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StartupJob stopped");
            foreach (var action in _actions)
            {
                action.Stop();
            }
            await base.StopAsync(stoppingToken);
        }

        private async Task RunJob(CancellationToken stoppingToken)
        {
            foreach (var action in _actions)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                try
                {
                    _logger.LogInformation("Running startup action. ActionName: {action}", action.Name);
                    await action.ExecuteAsync(GetStartupActionProvider(), stoppingToken);
                    _logger.LogInformation("Startup action completed. ActionName: {action}", action.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Startup action failed, shutting down");
                    throw;
                }
            }
        }

        private IServiceProvider GetStartupActionProvider() => _serviceProvider.CreateScope().ServiceProvider;
    }
}
