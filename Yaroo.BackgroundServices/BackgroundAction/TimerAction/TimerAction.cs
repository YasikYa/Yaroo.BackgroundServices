using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public abstract class TimerAction : BackgroundActionBase, IBackgroundAction<TimerActionInput>
    {
        public const string IterationRunningStatus = "Default:ProcessingActionOnTimer";
        public const string IterationCompletedStatus = "Default:WaitingForTimerTrigger";

        public override string Type => "TimerAction";

        public async Task ExecuteAsync(TimerActionInput input, IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(IterationRunningStatus);
            await ExecuteOnTimerTrigger(services, stoppingToken);
            UpdateStatus(IterationCompletedStatus);
        }

        protected abstract Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken);
    }
}
