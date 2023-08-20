namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public abstract class TimerAction : IBackgroundAction<TimerActionInput>
    {
        public async Task ExecuteAsync(TimerActionInput input, IServiceProvider services, CancellationToken stoppingToken)
        {
            await ExecuteOnTimerTrigger(services, stoppingToken);
        }

        protected abstract Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken);
    }
}
