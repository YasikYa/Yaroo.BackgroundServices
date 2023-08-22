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
            var (nextInterval, resetSignal) = _scheduler.GetNextWaitInterval();
            try
            {
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, resetSignal);
                await Task.Delay(nextInterval, linkedCts.Token);
            }
            catch (TaskCanceledException)
            {
                if (cancellationToken.IsCancellationRequested)
                    return TimerActionInput.Default;

                return await WaitForTrigger(services, cancellationToken);
            }

            return TimerActionInput.Default;
        }
    }
}
