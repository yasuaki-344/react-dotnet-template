# extract environment
FROM node:18-alpine3.16 AS extract
WORKDIR /app
COPY ./src/Frontend ./
SHELL ["/bin/ash", "-o", "pipefail", "-c"]
RUN find ./ -mindepth 2 -maxdepth 2 \
    ! -name package.json -print0 \
    | xargs rm -rf

# build environment
FROM node:18-alpine3.16 AS build

WORKDIR /app
COPY --from=extract /app ./
RUN npm i
COPY ./src/Frontend ./
RUN npm run build

# runtime envrionment
FROM nginx:1.20.2-alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build /app/ClientApp/build .

ENTRYPOINT ["nginx", "-g", "daemon off;"]
