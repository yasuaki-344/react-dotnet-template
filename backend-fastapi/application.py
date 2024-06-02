"""Application module."""
from fastapi import FastAPI
from containers import Container
import endpoints


def create_app() -> FastAPI:
    container = Container()

    app = FastAPI()
    app.container = container  # type: ignore
    app.include_router(endpoints.router)
    return app


app = create_app()
