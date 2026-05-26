[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md

# Service Vless Proxy

Service application for managing Vless+Reality connections with a simple HTTP API. (Golang + gin)

## Configuration

`api: port: "8030"` — port used by the HTTP API (or via the `API_PORT` environment variable)

`wg: port: "444"` — port used for establishing Vless connections (or via the `SING_BOX_PORT` environment variable)

> Additional environment variables required for the service are listed in [.env.template](.env.template):

`SERVICE_HOST` — host address where the service will run

`SERVICE_USER` and `SERVICE_PASSWORD` — username and password for HTTP API basic auth

## Deploy (Docker)

To run the service inside a Docker container, use the following command:

```bash
docker run -d \
  -p 8030:8030 \
  -p 444:444 \
  --env-file .env \   # if using a .env file
  -v $(pwd)/data:/app/data \
  vlessproxy
```
