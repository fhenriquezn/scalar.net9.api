using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace scalar.net8.api.Controllers
{
    [ApiController]
    [Authorize]
    [Asp.Versioning.ApiVersion(1)]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger = logger;

        /// <summary>
        /// Get Weather Forecast 
        /// </summary>
        /// <returns> List of WeatherForecast</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/WeatherForecast
        ///     
        /// </remarks>
        /// <response code="200">Return list of WeatherForecast</response>
        /// <response code="401">Not Authorized</response>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
