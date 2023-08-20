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

        protected override async Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken)
        {
            await Task.Delay(2000);
            _logger.LogInformation("SimpleTimerAction executed");
        }
    }
}
