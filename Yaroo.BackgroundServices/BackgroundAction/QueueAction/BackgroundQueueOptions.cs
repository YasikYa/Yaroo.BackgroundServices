using System.Threading.Channels;

namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public sealed class BackgroundQueueOptions
    {
        public bool IsBounded { get; set; }

        public int Capacity { get; set; } = 100;

        public BoundedChannelFullMode FullModeBehavior { get; set; } = BoundedChannelFullMode.Wait;
    }
}
