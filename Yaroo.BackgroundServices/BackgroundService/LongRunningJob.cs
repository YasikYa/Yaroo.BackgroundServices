using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yaroo.BackgroundServices.BackgroundAction;
using BaseBackgroundService = Microsoft.Extensions.Hosting.BackgroundService;

namespace Yaroo.BackgroundServices.BackgroundService
{
    public class LongRunningJob<TAction, TIterationInput> : BaseBackgroundService where TAction : IBackgroundAction<TIterationInput>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TAction _action;
        private readonly IBackgroundActionTrigger<TAction, TIterationInput> _actionTrigger;
        private readonly ILogger<TAction> _logger;

        public LongRunningJob(IServiceProvider serviceProvider, TAction action, IBackgroundActionTrigger<TAction, TIterationInput> actionTrigger, ILogger<TAction> logger)
        {
            _serviceProvider = serviceProvider;
            _action = action;
            _actionTrigger = actionTrigger;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Long running job is starting");
            await RunJob(stoppingToken);
            _logger.LogInformation("Long  running job completed");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Long running job is stopped");

            await base.StopAsync(stoppingToken);
        }

        private async Task RunJob(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var iterationScopeProvider = GetIterationScopedProvider();

                    _logger.LogTrace("Starting new iteration - waiting for trigger");
                    var iterationInput = await _actionTrigger.WaitForTrigger(iterationScopeProvider, stoppingToken);
                    _logger.LogTrace("Triggering with input {inputData}", iterationInput);
                    await _action.ExecuteAsync(iterationInput, iterationScopeProvider, stoppingToken);
                    _logger.LogTrace("Iteration completed");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job iteration failed with exception - keep running");
                }
            }
        }

        private IServiceProvider GetIterationScopedProvider() => _serviceProvider.CreateScope().ServiceProvider;
    }
}
