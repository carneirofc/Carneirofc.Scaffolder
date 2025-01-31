using Asp.Versioning;
using Carneirofc.Scaffold.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carneirofc.Scaffold.Web.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/[controller]")]
    public class WeatherReportController : ControllerBase
    {
        private readonly ILogger<WeatherReportController> _logger;
        private readonly IWeatherReportService _weatherReportService;

        public WeatherReportController(ILogger<WeatherReportController> logger, IWeatherReportService weatherReportService)
        {
            _logger = logger;
            _weatherReportService = weatherReportService;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult> GetWeatherReport([FromRoute] string city, CancellationToken cancellationToken)
        {
            var report = await _weatherReportService.GetWeatherReport(city, cancellationToken);
            return Ok(report);
        }
    }
}