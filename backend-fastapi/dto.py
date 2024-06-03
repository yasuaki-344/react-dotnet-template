"""DTO modules."""
from pydantic import BaseModel
from datetime import datetime


class WeatherForecast(BaseModel):
    date: datetime
    temperature_c: int
    temperature_f: int
    summary: str
