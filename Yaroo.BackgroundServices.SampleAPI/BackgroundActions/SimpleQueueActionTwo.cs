using Yaroo.BackgroundServices.BackgroundAction.QueueAction;
using Yaroo.BackgroundServices.SampleAPI.Models;

namespace Yaroo.BackgroundServices.SampleAPI.BackgroundActions
{
    public class SimpleQueueActionTwo : QueueAction<SimpleBackgroundWorkItem>
    {
        private readonly ILogger<SimpleQueueActionOne> _logger;

        public SimpleQueueActionTwo(ILogger<SimpleQueueActionOne> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteOnDequeue(IServiceProvider services, SimpleBackgroundWorkItem message, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queue handler {handler} recieved message: {message}", this.GetType().Name, message.Data);
            return Task.CompletedTask;
        }
    }
}
