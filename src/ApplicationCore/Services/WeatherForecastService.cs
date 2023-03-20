using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IMapper _mapper;
    private readonly IWeatherForecastRepository _repository;

    /// <summary>
    /// Initializes a new instance of WeatherForecastService class.
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="repository"></param>
    public WeatherForecastService(
        IMapper mapper,
        IWeatherForecastRepository repository
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<ICollection<WeatherForecastDto>> GetWeatherForecastsAsync()
    {
        var entities = await _repository.GetWeatherForecastsAsync();
        var result = _mapper.Map<ICollection<WeatherForecast>, ICollection<WeatherForecastDto>>(entities);

        return result;
    }
}

