namespace Yaroo.BackgroundServices.BackgroundAction.QueueAction
{
    public abstract class QueueAction<TMessage> : BackgroundActionBase, IBackgroundAction<TMessage>
    {
        public override string Type => "QueueAction";

        public async Task ExecuteAsync(TMessage input, IServiceProvider services, CancellationToken stoppingToken)
        {
            UpdateStatus(DefaultStatuses.QueueAction.Running);
            await ExecuteOnDequeue(services, input, stoppingToken);
            UpdateStatus(DefaultStatuses.QueueAction.WaitingForMessage);
        }

        protected abstract Task ExecuteOnDequeue(IServiceProvider services, TMessage message, CancellationToken stoppingToken);
    }
}
