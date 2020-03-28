using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.SerilogDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();

            _logger.LogTrace("this is a trace log");
            _logger.LogDebug("this is a debug log");
            _logger.LogInformation(1001, "This is information log");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("this is error log");
            _logger.LogCritical("this is a critical log");

            //Why not string interpolation
            _logger.LogInformation("This request is being handled at {Time}", DateTime.UtcNow);


            var temp = rng.Next(-20, 55);
            if (temp < 0)
            {
                try
                {
                    throw new Exception("This is exception message");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "This is an exception log");
                    return BadRequest();
                }
            }

            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
