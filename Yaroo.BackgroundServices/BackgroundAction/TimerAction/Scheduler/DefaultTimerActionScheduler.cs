using Microsoft.Extensions.Options;
using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction.Scheduler
{
    public class DefaultTimerActionScheduler<TAction> : IScheduler<TAction> where TAction : TimerAction
    {
        private readonly TimeSpan _defaultDelay;
        private ScheduleOverride? _scheduleOverride;
        private CancellationTokenSource? _currentResetSignal;

        public DefaultTimerActionScheduler(IOptionsMonitor<TimerActionOptions> options)
        {
            var configuredOptions = options.Get(TypeNameHelper.GetTypeName<TAction>());
            _defaultDelay = TimeSpan.FromSeconds(configuredOptions.IterationDelaySeconds);
        }

        public (TimeSpan nextInterval, CancellationToken resetSignal) GetNextWaitInterval()
        {
            _currentResetSignal = new CancellationTokenSource();
            if (_scheduleOverride is not null)
            {
                var nextInterval = _scheduleOverride.GetNextWaitInterval();
                _scheduleOverride = null;
                return (nextInterval, _currentResetSignal.Token);
            }

            return (GetNextComputedInterval(_defaultDelay), _currentResetSignal.Token);
        }

        protected virtual TimeSpan GetNextComputedInterval(TimeSpan defaultDelay) => defaultDelay;

        public void ScheduleNextOverride(DateTimeOffset scheduleOn)
        {
            _scheduleOverride = new ScheduleOverride(scheduleOn);
            _currentResetSignal?.Cancel();
        }

        public void ScheduleNextOverride(TimeSpan timeout)
        {
            _scheduleOverride = new ScheduleOverride(timeout);
            _currentResetSignal?.Cancel();
        }

        private class ScheduleOverride
        {
            private readonly TimeSpan _timeout;
            private readonly DateTimeOffset _scheduleOn;

            public ScheduleOverride(TimeSpan timeout)
            {
                _timeout = timeout;
                _scheduleOn = DateTimeOffset.UnixEpoch;
            }

            public ScheduleOverride(DateTimeOffset scheduleOn)
            {
                _scheduleOn = scheduleOn;
                _timeout = TimeSpan.Zero;
            }

            public TimeSpan GetNextWaitInterval()
            {
                if (_timeout != TimeSpan.Zero)
                    return _timeout;

                var plannedDelay = DateTimeOffset.UtcNow.Subtract(_scheduleOn);
                return plannedDelay > TimeSpan.Zero ? plannedDelay : TimeSpan.Zero;
            }
        }
    }
}
