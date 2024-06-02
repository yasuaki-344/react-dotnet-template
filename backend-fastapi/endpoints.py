"""Endpoints modules."""

from fastapi import APIRouter
from dependency_injector.wiring import inject

router = APIRouter(tags=["WeatherForecast"])


@router.get("/api/v1/weather-forecasts")
@inject
def root():
    return {"message": "Hello World"}
