from typing import List
from datetime import datetime
from dto import WeatherForecast


class WeatherForecastService:
    def __init__(self) -> None:
        pass

    def get_weather_forecast_list(self) -> List[WeatherForecast]:
        return [
            {
                "date": datetime.now(),
                "temperature_c": 10,
                "temperature_f": 20,
                "summary": "test",
            },
            {
                "date": datetime.now(),
                "temperature_c": 30,
                "temperature_f": 40,
                "summary": "test",
            }
        ]
