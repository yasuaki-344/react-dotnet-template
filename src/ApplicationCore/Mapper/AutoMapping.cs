﻿using ApplicationCore.Dto;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.Mapper
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
