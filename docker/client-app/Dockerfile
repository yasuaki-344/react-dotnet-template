# extract environment
FROM node:22-alpine3.19 AS extract
WORKDIR /app
COPY ./frontend/src ./
SHELL ["/bin/ash", "-o", "pipefail", "-c"]
RUN find ./ -mindepth 2 -maxdepth 2 \
    ! -name package.json -print0 \
    | xargs rm -rf

# build environment
FROM node:22-alpine3.19 AS build

WORKDIR /app
COPY --from=extract /app ./
RUN npm i
COPY ./frontend/src ./
RUN npm run build

# runtime envrionment
FROM nginx:1.26.0-alpine3.19
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build /app/ClientApp/build .

ENTRYPOINT ["nginx", "-g", "daemon off;"]
