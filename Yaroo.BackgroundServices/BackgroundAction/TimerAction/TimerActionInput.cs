namespace Yaroo.BackgroundServices.BackgroundAction.TimerAction
{
    public struct TimerActionInput
    {
        private static TimerActionInput _default = new TimerActionInput();
        public static TimerActionInput Default => _default;
    }
}
