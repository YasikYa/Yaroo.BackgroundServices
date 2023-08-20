using Microsoft.Extensions.Options;
using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public class TimerActionTrigger<TAction> : IBackgroundActionTrigger<TAction, TimerActionInput>
        where TAction : TimerAction
    {
        private readonly int _defaultDelay;

        public TimerActionTrigger(IOptionsMonitor<TimerActionOptions> options)
        {
            _defaultDelay = options.Get(TypeNameHelper.GetTypeName<TAction>()).IterationDelaySeconds;
        }

        public async Task<TimerActionInput> WaitForTrigger(IServiceProvider services, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(_defaultDelay));
            return TimerActionInput.Default;
        }
    }
}
