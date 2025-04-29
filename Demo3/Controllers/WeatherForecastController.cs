using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            // µo°e¨Æ¥ó
            await _mediator.Publish(new MemberUpgradedEvent("Nathan", "5"));
            return Ok();
        }
    }
}