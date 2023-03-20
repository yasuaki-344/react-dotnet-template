using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ApplicationCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("/api/v{version:apiVersion}/weather-forecasts")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
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

    /// <summary>
    /// Gets weather forecast information
    /// </summary>
    /// <returns>Weather forecast</returns>
    /// <response code="200">Returns weather forecast</response>
    /// <response code="500">Internal server error</response>
    [HttpGet(Name = "getWeatherForecasts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WeatherForecastDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public IEnumerable<WeatherForecastDto> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

