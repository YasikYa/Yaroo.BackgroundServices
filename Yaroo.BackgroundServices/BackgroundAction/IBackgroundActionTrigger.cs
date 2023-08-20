namespace Yaroo.BackgroundServices.BackgroundAction
{
    public interface IBackgroundActionTrigger<TAction, TIterationInput> where TAction : IBackgroundAction<TIterationInput>
    {
        Task<TIterationInput> WaitForTrigger(IServiceProvider services, CancellationToken cancellationToken);
    }
}
