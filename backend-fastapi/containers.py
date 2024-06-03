"""Containers module."""
from dependency_injector import containers, providers
import services


class Container(containers.DeclarativeContainer):
    wiring_config = containers.WiringConfiguration(modules=["endpoints"])

    weather_forecast_service = providers.Factory(
        services.WeatherForecastService
    )
