using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
           foreach(var i in this.HttpContext.User.Claims){
               var str=$"Issuer:{i.Issuer} \n";
               str+=$"OriginalIssuer:{i.OriginalIssuer} \n";
               str+=$"Properties:{i.Properties} \n";
               str+=$"Subject:{i.Subject} \n";
               str+=$"Type:{i.Type} \n";
               str+=$"Value:{i.Value} \n";
               str+=$"ValueType:{i.ValueType} \n";
               _logger.LogInformation("{0}",str);
           }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
