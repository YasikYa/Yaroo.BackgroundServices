namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public abstract class TimerAction : BackgroundActionBase, IBackgroundAction<TimerActionInput>
    {
        public override string Type => "TimerAction";

        public async Task ExecuteAsync(TimerActionInput input, IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(DefaultStatuses.TimerAction.Running);
            await ExecuteOnTimerTrigger(services, stoppingToken);
            UpdateStatus(DefaultStatuses.TimerAction.WaitingForTrigger);
        }

        protected abstract Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken);
    }
}
