using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WEB_API_BETA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : Controller
    {
        private readonly ILogger<HealthCheckController> _logger;
        private static int requestCounter;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            _logger.LogInformation("API BETA - healthcheck/check - request started");
            _logger.LogInformation("API BETA - healthcheck/check - current request counter: {RequestCounter}", requestCounter);

            if (requestCounter <= 5)
            {
                requestCounter++;
                _logger.LogWarning("API BETA - healthcheck/check - request error 500. Updated request counter: {RequestCounter}", requestCounter);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error");
            }


            _logger.LogInformation("API BETA - healthcheck/check - request accepted.");
            _logger.LogInformation("  ");
            return Ok("Request accepted.");
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            _logger.LogInformation("API BETA - healthcheck/reset - request started");
            requestCounter = 0;
            _logger.LogInformation("API BETA - healthcheck/reset - request counter reset to zero.");
            _logger.LogInformation("  ");

            return Ok("Request counter reset.");
        }
    }
}
