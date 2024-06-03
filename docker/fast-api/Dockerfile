FROM python:3.12
WORKDIR /source
COPY backend-fastapi/requirements.txt .
RUN pip install --no-cache-dir --upgrade -r requirements.txt

COPY backend-fastapi app

CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "80"]
