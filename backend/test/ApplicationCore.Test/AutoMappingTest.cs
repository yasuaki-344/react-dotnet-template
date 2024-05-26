using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Mapper;
using AutoMapper;
using Xunit;

namespace ApplicationCore.Test;

public class AutoMappingTest
{
    private readonly IMapper _mapper;

    public AutoMappingTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapping>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Map_GivenWeatherForecastEntity_ReturnDto()
    {
        var expect = new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = 30,
            Summary = "summary",
        };
        var actual = _mapper.Map<WeatherForecastDto>(expect);

        Assert.Equal(expect.Date, actual.Date);
        Assert.Equal(expect.TemperatureC, actual.TemperatureC);
        Assert.Equal(expect.Summary, actual.Summary);
    }
}
