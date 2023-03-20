FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /source
COPY src/ApplicationCore/ ApplicationCore/.
COPY src/Infrastructure/ Infrastructure/.
COPY src/WebApi/ WebApi/.
WORKDIR /source/WebApi
RUN dotnet restore && \
    dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
RUN adduser -D appuser

WORKDIR /app
COPY --from=build /app ./

USER appuser
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["./WebApi"]
