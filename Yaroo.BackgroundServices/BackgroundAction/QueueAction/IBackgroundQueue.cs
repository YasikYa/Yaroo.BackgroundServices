namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public interface IBackgroundQueue<TMessage>
    {
        Task Enqueue(TMessage message);

        Task<TMessage> DequeueAsync(CancellationToken cancellationToken);
    }
}
