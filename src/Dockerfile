FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN apk update && apk add --no-cache nodejs npm

WORKDIR /source
COPY ./ApplicationCore/ ApplicationCore/.
COPY ./Infrastructure/ Infrastructure/.
COPY ./Web/ Web/.
WORKDIR /source/Web
RUN dotnet restore && \
    dotnet publish -c release -o /app --no-restore

