[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

# EasyVPN frontend

A web interface application with a simple UI. (TypeScript + React)

## Configuration

> The web interface configuration is located in [`config.ts`](./src/config.ts), substituted by enviroment variables:

`REACT_APP_API_URL=https://localhost:80/api/` - URL to the deployed [`EasyVPN backend`](../backend/README.md), with which the web interface communicates

`REACT_APP_AUTH_CHECK_MINUTES=15` - frequency in minutes with which authorization is automatically checked
(authorization is also automatically checked when navigation changes)

## Deploy (docker)

To run the service in a docker container, use the following command:

```bash
docker run -d \
--name frontend \
-p 3000:3000 `#your port` \
-e REACT_APP_API_URL=https://localhost:80/api/ \
 -e REACT_APP_AUTH_CHECK_MINUTES=15 \
 easyvpn/frontend:latest
```
