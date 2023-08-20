using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction
{
    public abstract class BackgroundActionBase : IBackgroundAction
    {
        public const string StoppedStatus = "Default:Stopped";
        private string _status = "Default:ActionInitialized";

        public virtual string Name => TypeNameHelper.GetTypeName(GetType());
        public abstract string Type { get; }
        public string Status => _status;

        protected void UpdateStatus(string status) => _status = status;

        public void Stop() => UpdateStatus(StoppedStatus);
    }
}
