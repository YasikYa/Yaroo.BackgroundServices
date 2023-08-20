using Microsoft.Extensions.DependencyInjection;
using Yaroo.BackgroundServices.BackgroundAction;
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
            services.AddSingleton<IBackgroundAction<TActionIterationInput>, TAction>();
            services.AddSingleton<IBackgroundActionTrigger<TAction, TActionIterationInput>, TTrigger>();
            services.AddHostedService<LongRunningJob<TAction, TActionIterationInput>>();
            return services;
        }

        public static IServiceCollection RegisterTimerAction<TAction>(this IServiceCollection services, Action<TimerActionOptions> configureOptions)
            where TAction : TimerAction
        {
            services.Configure(TypeNameHelper.GetTypeName<TAction>(), configureOptions);
            return services.RegisterBackgroundJob<TAction, TimerActionTrigger<TAction>, TimerActionInput>();
        }
    }
}
