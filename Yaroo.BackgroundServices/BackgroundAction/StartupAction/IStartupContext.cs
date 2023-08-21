namespace Yaroo.BackgroundServices.BackgroundAction.StartupAction
{
    public interface IStartupContext
    {
        bool IsClosed { get; }
    }
}
