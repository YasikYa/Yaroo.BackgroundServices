using Yaroo.BackgroundServices.SampleAPI.BackgroundActions;
using Yaroo.BackgroundServices.Extensions;
using Yaroo.BackgroundServices.Middleware;
using Yaroo.BackgroundServices.SampleAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOptions();

services.RegisterTimerAction<SimpleTimerAction>(o =>
{
    o.IterationDelaySeconds = 10;
});

services.RegisterTimerAction<CustomScheduledAction, CustomScheduler>(o => { });

services.RegisterStartupAction<SimpleStartupAction>();

services.RegisterBackgroundQueue<SimpleBackgroundWorkItem>()
    .AddHandler<SimpleQueueActionOne>()
    .AddHandler<SimpleQueueActionTwo>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStartupActionMiddleware();
app.UseRouting();
app.MapControllers();

app.Run();
