using Microsoft.Extensions.Options;
using Yaroo.BackgroundServices.BackgroundAction.TimerAction;
using Yaroo.BackgroundServices.BackgroundAction.TimerAction.Scheduler;

namespace Yaroo.BackgroundServices.SampleAPI.BackgroundActions
{
    public class CustomScheduledAction : TimerAction
    {
        private readonly ILogger<CustomScheduledAction> _logger;

        public CustomScheduledAction(ILogger<CustomScheduledAction> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteOnTimerTrigger(IServiceProvider services, CancellationToken stoppingToken)
        {
            _logger.LogInformation("CustomScheduledAction is executed");
            return Task.CompletedTask;
        }
    }

    public class CustomScheduler : DefaultTimerActionScheduler<CustomScheduledAction>
    {
        private readonly IEnumerator<int> _intervals;

        public CustomScheduler(IOptionsMonitor<TimerActionOptions> options) : base(options)
        {
            _intervals = GetIntervals(new List<int> { 1, 3, 5, 7, 9 }).GetEnumerator();
        }

        protected override TimeSpan GetNextComputedInterval(TimeSpan defaultDelay)
        {
            var result = TimeSpan.FromSeconds(_intervals.Current);
            _intervals.MoveNext();

            return result;
        }

        private IEnumerable<int> GetIntervals(List<int> intervals)
        {
            var next = 0;
            while (true)
            {
                yield return intervals[next % intervals.Count];
                next++;
            }
        }
    }
}
