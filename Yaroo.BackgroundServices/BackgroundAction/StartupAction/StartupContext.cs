namespace Yaroo.BackgroundServices.BackgroundAction.StartupAction
{
    public class StartupContext : IStartupContext
    {
        private readonly IEnumerable<IStartupAction> _actions;
        private bool _allCompleted = false;

        public StartupContext(IEnumerable<IStartupAction> actions)
        {
            _actions = actions;
        }

        public bool IsClosed => IsClosedCached();

        private bool IsClosedCached()
        {
            if (_allCompleted)
                return true;

            _allCompleted = _actions.All(sa => sa.Status == DefaultStatuses.StartupAction.Completed);
            return _allCompleted;
        }
    }
}
