using Microsoft.AspNetCore.Mvc;
using Yaroo.BackgroundServices.BackgroundAction;
using Yaroo.BackgroundServices.BackgroundAction.QueueAction;
using Yaroo.BackgroundServices.BackgroundAction.TimerAction.Scheduler;
using Yaroo.BackgroundServices.SampleAPI.BackgroundActions;
using Yaroo.BackgroundServices.SampleAPI.Models;

namespace Yaroo.BackgroundServices.SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [ProducesResponseType(typeof(List<BackgroudServicesStatus>), StatusCodes.Status200OK)]
        [HttpGet]
        public Task<IActionResult> GetBackgroundStatus([FromServices]ILogger<TestController> logger, [FromServices]IStatusCollector statusCollector)
        {
            logger.LogInformation("Recieved request for getting background job statuses");
            var statuses = statusCollector.CollectStatuses();
            var response = statuses.Select(s => new BackgroudServicesStatus { ActionName = s.Name, Status = s.Status, ActionType = s.Type });
            return Task.FromResult(Ok(response) as IActionResult);
        }

        [HttpPost("enqueue")]
        public async Task<IActionResult> QueueBackgroundMessage([FromBody]string message, [FromServices]IBackgroundQueue<string> queue)
        {
            await queue.Enqueue(message);
            return Ok();
        }

        [HttpPost("reset-custom-timer")]
        public async Task<IActionResult> ForseTriggerTimerAction([FromServices]IScheduler<SimpleTimerAction> scheduler)
        {
            scheduler.ScheduleNextOverride(TimeSpan.FromMilliseconds(2000));
            return Ok();
        }
    }
}
