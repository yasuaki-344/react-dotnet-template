FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN apk update && apk add --no-cache nodejs npm

WORKDIR /source/Web/ClientApp
COPY ./Web/ClientApp/package*.json ./
RUN npm install

WORKDIR /source
COPY ./ApplicationCore/ ApplicationCore/.
COPY ./Infrastructure/ Infrastructure/.
COPY ./Web/ Web/.
WORKDIR /source/Web
RUN dotnet restore && \
    dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
RUN apk add --update sudo \
    && adduser -D appuser \
    && echo "appuser ALL=(ALL) NOPASSWD: ALL" > /etc/sudoers.d/appuser \
    && chmod 0440 /etc/sudoers.d/appuser

USER appuser

COPY --from=build /app ./
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "Web.dll"]
