FROM ubuntu:22.04 AS build
ENV ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get update && apt-get install -y --no-install-recommends \
    dotnet6=6.0.127-0ubuntu1~22.04.1 \
    ca-certificates=20230311ubuntu0.22.04.1 \
    tzdata=2024a-0ubuntu0.22.04

WORKDIR /source
COPY backend/src/ApplicationCore/ ApplicationCore/.
COPY backend/src/Infrastructure/ Infrastructure/.
COPY backend/src/WebApi/ WebApi/.

WORKDIR /source/WebApi
RUN dotnet restore && \
    dotnet publish -c Release -r ubuntu.22.04-x64 --self-contained false -o /app --no-restore

FROM ubuntu/dotnet-aspnet:6.0-22.04_69

WORKDIR /app
COPY --from=build /usr/share/zoneinfo /usr/share/zoneinfo
COPY --from=build /app ./

ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "./WebApi.dll"]
