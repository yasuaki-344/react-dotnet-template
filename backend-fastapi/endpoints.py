"""Endpoints modules."""
from typing import List
from fastapi import APIRouter, Depends
from dependency_injector.wiring import inject, Provide
from dto import WeatherForecast
from services import WeatherForecastService
from containers import Container


router = APIRouter(tags=["WeatherForecast"])


@router.get("/api/v1/weather-forecasts", response_model=List[WeatherForecast])
@inject
def get_weather_forecast_list(
    weather_forecast_service: WeatherForecastService = Depends(Provide[Container.weather_forecast_service])
) -> List[WeatherForecast]:
    return weather_forecast_service.get_weather_forecast_list()
