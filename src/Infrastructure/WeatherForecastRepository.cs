using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of WeatherForecastRepository class.
    /// </summary>
    /// <param name="context"></param>
    public WeatherForecastRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ICollection<WeatherForecast>> GetWeatherForecastsAsync()
    {
        var entities = await _context.WeatherForecasts
            .AsNoTracking()
            .ToListAsync();
        return entities;
    }
}
