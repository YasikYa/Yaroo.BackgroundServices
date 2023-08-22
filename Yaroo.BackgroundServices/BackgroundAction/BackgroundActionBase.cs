using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction
{
    public abstract class BackgroundActionBase : IBackgroundAction
    {
        private string _status = DefaultStatuses.Common.Initialized;

        public virtual string Name => TypeNameHelper.GetTypeName(GetType());
        public abstract string Type { get; }
        public string Status => _status;

        protected void UpdateStatus(string status) => _status = status;

        public void Stop() => UpdateStatus(DefaultStatuses.Common.Stopped);
    }
}
