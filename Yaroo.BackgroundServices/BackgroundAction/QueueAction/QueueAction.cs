namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public abstract class QueueAction<TMessage> : BackgroundActionBase, IBackgroundAction<TMessage>
    {
        public const string ProcessingMessage = "Default:ProcessingMessage";
        public const string WaitingForMessage = "Default:WaitingForMessage";

        public override string Type => "QueueAction";

        public async Task ExecuteAsync(TMessage input, IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(ProcessingMessage);
            await ExecuteOnDequeue(services, input, stoppingToken);
            UpdateStatus(WaitingForMessage);
        }

        protected abstract Task ExecuteOnDequeue(IServiceProvider services, TMessage message, CancellationToken stoppingToken);
    }
}
