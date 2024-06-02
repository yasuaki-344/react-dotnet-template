"""Endpoints modules."""

from fastapi import APIRouter
from dependency_injector.wiring import inject


router = APIRouter()


@router.get("/")
@inject
def root():
    return {"message": "Hello World"}
