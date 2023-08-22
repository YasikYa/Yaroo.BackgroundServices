using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yaroo.BackgroundServices.BackgroundAction;
using BaseBackgroundService = Microsoft.Extensions.Hosting.BackgroundService;

namespace Yaroo.BackgroundServices.BackgroundService
{
    public class LongRunningJob<TAction, TIterationInput> : BaseBackgroundService where TAction : IBackgroundAction<TIterationInput>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<TAction> _actions;
        private readonly IBackgroundActionTrigger<TAction, TIterationInput> _actionTrigger;
        private readonly ILogger<LongRunningJob<TAction, TIterationInput>> _logger;

        public LongRunningJob(IServiceProvider serviceProvider, IEnumerable<TAction> actions, IBackgroundActionTrigger<TAction, TIterationInput> actionTrigger, ILogger<LongRunningJob<TAction, TIterationInput>> logger)
        {
            _serviceProvider = serviceProvider;
            _actions = actions;
            _actionTrigger = actionTrigger;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Long running job is starting. Actions: {actions}", _actions.Select(a => a.Name).ToArray());
            await RunJob(stoppingToken);
            _logger.LogInformation("Long  running job completed. Actions: {actions}", _actions.Select(a => a.Name).ToArray());
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Long running job is stopped. Actions: {actions}", _actions.Select(a => a.Name).ToArray());
            foreach (var action in _actions)
            {
                action.Stop();
            }
            await base.StopAsync(stoppingToken);
        }

        private async Task RunJob(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var iterationScopeProvider = GetIterationScopedProvider();
                    _logger.LogTrace("Starting new iteration - waiting for trigger. Actions: {actions}", _actions.Select(a => a.Name).ToArray());
                    var iterationInput = await _actionTrigger.WaitForTrigger(iterationScopeProvider, stoppingToken);
                    
                    foreach ( var action in _actions)
                    {
                        try
                        {
                            var actionScopeProvider = GetIterationScopedProvider();
                            _logger.LogTrace("Triggering with input {inputData}. Action: {action}", iterationInput, action.Name);
                            await action.ExecuteAsync(iterationInput, actionScopeProvider, stoppingToken);
                            _logger.LogTrace("Iteration completed. Action: {action}", action.Name);
                        }
                        catch ( Exception ex )
                        {
                            _logger.LogError(ex, "Action iteration failed with exception. Action: {action}", action.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job iteration failed with exception - keep running. Actions: {actions}", _actions.Select(a => a.Name).ToArray());
                }
            }
        }

        private IServiceProvider GetIterationScopedProvider() => _serviceProvider.CreateScope().ServiceProvider;
    }
}
