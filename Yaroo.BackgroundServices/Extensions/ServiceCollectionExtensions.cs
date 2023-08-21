using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yaroo.BackgroundServices.BackgroundAction;
using Yaroo.BackgroundServices.BackgroundAction.StartupAction;
using Yaroo.BackgroundServices.BackgroundAction.TimerAction;
using Yaroo.BackgroundServices.BackgroundService;
using Yaroo.BackgroundServices.Utility;

namespace Yaroo.BackgroundServices.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterBackgroundJob<TAction, TTrigger, TActionIterationInput>(
            this IServiceCollection services)
            where TAction : class, IBackgroundAction<TActionIterationInput>
            where TTrigger : class, IBackgroundActionTrigger<TAction, TActionIterationInput>
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.RegisterActionWithStatusCollector<TAction>();
            services.AddSingleton<IBackgroundAction<TActionIterationInput>>(sp => sp.GetRequiredService<TAction>());
            services.AddSingleton<IBackgroundActionTrigger<TAction, TActionIterationInput>, TTrigger>();
            services.AddHostedService<LongRunningJob<TAction, TActionIterationInput>>();
            return services;
        }

        public static IServiceCollection RegisterTimerAction<TAction>(this IServiceCollection services, Action<TimerActionOptions> configureOptions)
            where TAction : TimerAction
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.Configure(TypeNameHelper.GetTypeName<TAction>(), configureOptions);
            return services.RegisterBackgroundJob<TAction, TimerActionTrigger<TAction>, TimerActionInput>();
        }

        public static IServiceCollection RegisterStartupAction<TAction>(this IServiceCollection services)
            where TAction : StartupAction
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.RegisterActionWithStatusCollector<TAction>();
            services.TryAddSingleton<IStartupContext, StartupContext>();
            services.AddHostedService<StartupJob>();
            services.AddSingleton<IStartupAction>(sp => sp.GetRequiredService<TAction>());

            return services;
        }

        private static IServiceCollection RegisterActionWithStatusCollector<TAction>(this IServiceCollection services)
            where TAction : class, IBackgroundAction
        {
            services.TryAddSingleton<IStatusCollector, StatusCollector>();
            services.TryAddSingleton<TAction>();
            services.AddSingleton<IBackgroundAction>(sp => sp.GetRequiredService<TAction>());

            return services;
        }
    }
}
