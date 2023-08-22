using Microsoft.Extensions.Options;
using System.Threading.Channels;
using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public class BackgroundQueue<TMessage> : IBackgroundQueue<TMessage>
    {
        private readonly Channel<TMessage> _queue;

        public BackgroundQueue(IOptionsMonitor<BackgroundQueueOptions> options)
        {
            var channelConfiguration = options.Get(TypeNameHelper.GetTypeName<TMessage>());
            _queue = CreateChannel(channelConfiguration);
        }

        public async Task<TMessage> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }

        public async Task Enqueue(TMessage message)
        {
            await _queue.Writer.WriteAsync(message);
        }

        private static Channel<TMessage> CreateChannel(BackgroundQueueOptions options)
        {
            if (!options.IsBounded)
            {
                var channelOptions = new UnboundedChannelOptions
                {
                    SingleReader = true,
                };
                return Channel.CreateUnbounded<TMessage>(channelOptions);
            }
            else
            {
                var channelOptions = new BoundedChannelOptions(options.Capacity)
                {
                    SingleReader = true,
                    FullMode = options.FullModeBehavior
                };
                return Channel.CreateBounded<TMessage>(channelOptions);
            }
        }
    }
}
