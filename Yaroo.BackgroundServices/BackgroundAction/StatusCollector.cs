using Microsoft.Extensions.DependencyInjection;

namespace Yaroo.BackgroundServices.BackgroundAction
{
    public sealed record BackgroundActionStatus(string Name, string Status, string Type);

    public sealed class StatusCollector : IStatusCollector
    {
        private readonly IServiceProvider _services;

        public StatusCollector(IServiceProvider services)
        {
            _services = services;
        }

        public IEnumerable<BackgroundActionStatus> CollectStatuses()
        {
            var allActions = _services.GetServices<IBackgroundAction>();
            if (allActions.Any())
            {
                var result = new List<BackgroundActionStatus>();
                foreach (var action in allActions)
                {
                    result.Add(new(action.Name, action.Status, action.Type));
                }

                return result;
            }

            return Enumerable.Empty<BackgroundActionStatus>();
        }
    }
}
