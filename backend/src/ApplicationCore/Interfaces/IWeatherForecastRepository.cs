using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;

public interface IWeatherForecastRepository
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ICollection<WeatherForecast>> GetWeatherForecastsAsync();
}
