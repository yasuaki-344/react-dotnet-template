using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class WeatherForecastService : IWeatherForecastService
{
    /// <summary>
    /// Initializes a new instance of WeatherForecastService class.
    /// </summary>
    public WeatherForecastService()
    {
    }

    /// <inheritdoc/>
    public async Task<ICollection<WeatherForecastDto>> GetWeatherForecastsAsync()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

