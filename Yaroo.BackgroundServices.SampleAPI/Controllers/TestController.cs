using Microsoft.AspNetCore.Mvc;
using Yaroo.BackgroundServices.SampleAPI.Models;

namespace Yaroo.BackgroundServices.SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [ProducesResponseType(typeof(List<BackgroudServicesStatus>), StatusCodes.Status200OK)]
        [HttpGet]
        public Task<IActionResult> GetBackgroundStatus([FromServices]ILogger<TestController> logger)
        {
            logger.LogInformation("Recieved request for getting background job statuses");
            var response = new List<BackgroudServicesStatus>()
            {
                new BackgroudServicesStatus {ActionName = "TestAction", ActionType = "Timer", Status = "Running" }
            };
            return Task.FromResult(Ok(response) as IActionResult);
        }
    }
}
