using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Yaroo.BackgroundServices.BackgroundAction.StartupAction;

namespace Yaroo.BackgroundServices.Middleware
{
    public class StartupActionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStartupContext _startupContext;

        public StartupActionsMiddleware(RequestDelegate next, IStartupContext startupContext)
        {
            _next = next;
            _startupContext = startupContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_startupContext.IsClosed)
            {
                context.Response.StatusCode = 590;
                return;
            }

            await _next(context);
        }
    }

    public static class StartupActionMiddlewareExtensions
    {
        public static IApplicationBuilder UseStartupActionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<StartupActionsMiddleware>();
        }
    }
}
