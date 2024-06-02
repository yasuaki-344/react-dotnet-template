"""Containers module."""
from dependency_injector import containers, providers
import services


class Container(containers.DeclarativeContainer):
    wiring_config = containers.WiringConfiguration(modules=["endpoints"])

    my_service = providers.Factory(
        services.MyService
    )
