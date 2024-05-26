using ApplicationCore.Dto;

namespace ApplicationCore.Interfaces;

public interface IWeatherForecastService
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ICollection<WeatherForecastDto>> GetWeatherForecastsAsync();
}
