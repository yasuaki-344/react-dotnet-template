using ApplicationCore.Dto;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.Services
{
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Initializes a new instance of AutoMapping class.
        /// </summary>
        public AutoMapping()
        {
            CreateMap<WeatherForecast, WeatherForecastDto>();
        }
    }
}
