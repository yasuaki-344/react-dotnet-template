# build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /source
COPY ./ApplicationCore/ ApplicationCore/.
COPY ./Infrastructure/ Infrastructure/.
COPY ./WebApi/ WebApi/.
WORKDIR /source/WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o /app --no-restore -r linux-musl-x64 --self-contained

# runtime envrionment
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
RUN adduser -D appuser

USER appuser

WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["./WebApi"]