using Yaroo.BackgroundServices.BackgroundAction.TimerAction.Scheduler;

namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public class TimerActionTrigger<TAction> : IBackgroundActionTrigger<TAction, TimerActionInput>
        where TAction : TimerAction
    {
        private readonly IScheduler<TAction> _scheduler;

        public TimerActionTrigger(IScheduler<TAction> scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task<TimerActionInput> WaitForTrigger(IServiceProvider services, CancellationToken cancellationToken)
        {
            await Task.Delay(_scheduler.GetNextWaitInterval());
            return TimerActionInput.Default;
        }
    }
}
