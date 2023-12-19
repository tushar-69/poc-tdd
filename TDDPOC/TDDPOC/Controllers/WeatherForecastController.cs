using Microsoft.AspNetCore.Mvc;
using TDDPOC.Service;

namespace TDDPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("{summary}")]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySummary(string summary)
        {
            if (string.IsNullOrEmpty(summary))
                throw new ArgumentNullException(nameof(summary));

            var weatherForecast = _weatherForecastService.GetById(summary);
            if (weatherForecast == null)
                return NotFound();

            return Ok(new WeatherForecast { 
                Summary = weatherForecast.Summary,
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC
            });
        }
    }
}
