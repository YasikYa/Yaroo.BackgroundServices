namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction.Scheduler
{
    public interface IScheduler<TAction> where TAction : TimerAction
    {
        TimeSpan GetNextWaitInterval();

        void ScheduleNextOverride(DateTimeOffset scheduleOn);

        void ScheduleNextOverride(TimeSpan timeout);
    }
}
