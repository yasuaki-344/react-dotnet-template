# build environment
FROM node:18-alpine3.16 AS build

WORKDIR /app
COPY ./src/ClientApp/package.json ./
COPY ./src/ClientApp/package-lock.json ./
RUN npm i

COPY /src/ClientApp ./
RUN npm run build

# runtime envrionment
FROM nginx:1.20.2-alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build /app/build .

ENTRYPOINT ["nginx", "-g", "daemon off;"]
