"""Endpoints modules."""
from typing import List
from fastapi import APIRouter
from datetime import datetime
from dependency_injector.wiring import inject
from dto import WeatherForecast

router = APIRouter(tags=["WeatherForecast"])


@router.get("/api/v1/weather-forecasts", response_model=List[WeatherForecast])
@inject
def get_weather_forecast_list() -> List[WeatherForecast]:
    return [
        {
            "date": datetime.now(),
            "temperature_c": 10,
            "temperature_f": 20,
            "summary": "test",
        },
        {
            "date": datetime.now(),
            "temperature_c": 10,
            "temperature_f": 20,
            "summary": "test",
        }
    ]
