using Yaroo.BackgroundServices.BackgroundAction.TimerAction;

namespace Yaroo.BackgroundServices.SampleAPI.BackgroundActions
{
    public class SimpleTimerAction : TimerAction
    {
        private readonly ILogger<SimpleTimerAction> _logger;

        public SimpleTimerAction(ILogger<SimpleTimerAction> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken)
        {
            _logger.LogInformation("SimpleTimerAction executed");
            return Task.CompletedTask;
        }
    }
}
