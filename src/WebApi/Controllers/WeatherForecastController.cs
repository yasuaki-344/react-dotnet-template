using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ApplicationCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApplicationCore.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("/api/v{version:apiVersion}/weather-forecasts")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _service;

    /// <summary>
    /// Initializes a new instance of WeatherForecastController class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="service"></param>
    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IWeatherForecastService service
    )
    {

        _logger = logger;
        _service = service;
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
    public async Task<IActionResult> GetWeatherForecastsAsync()
    {
        var url = $"{HttpContext.Request.Path}{HttpContext.Request.QueryString.ToString()}";
        _logger.LogInformation(url);

        try
        {
            var result = await _service.GetWeatherForecastsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            var response = new ProblemDetails
            {
                Type = "about:blank",
                Title = "Unhandled error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = url,
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}

