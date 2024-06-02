"""Entry point."""
from fastapi import FastAPI


def create_app() -> FastAPI:
    app = FastAPI()
    return app


app = create_app()


@app.get("/")
async def root():
    return {"message": "Hello World"}
