using System.Threading.Channels;

namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public class BackgroundQueue<TMessage> : IBackgroundQueue<TMessage>
    {
        private readonly Channel<TMessage> _queue;

        public BackgroundQueue()
        {
            var options = new BoundedChannelOptions(100)
            {
                SingleReader = true,
            };
            _queue = Channel.CreateBounded<TMessage>(options);
        }

        public async Task<TMessage> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }

        public async Task Enqueue(TMessage message)
        {
            await _queue.Writer.WriteAsync(message);
        }
    }
}
