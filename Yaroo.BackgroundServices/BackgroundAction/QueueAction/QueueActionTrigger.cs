namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public class QueueActionTrigger<TMessage> : IBackgroundActionTrigger<QueueAction<TMessage>, TMessage>
    {
        private readonly IBackgroundQueue<TMessage> _queue;

        public QueueActionTrigger(IBackgroundQueue<TMessage> queue)
        {
            _queue = queue;
        }

        public async Task<TMessage> WaitForTrigger(IServiceProvider services, CancellationToken cancellationToken)
        {
            return await _queue.DequeueAsync(cancellationToken);
        }
    }
}
