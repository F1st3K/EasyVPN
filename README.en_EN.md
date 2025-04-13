[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md


# EasyVPN
A simple VPN + Backend + Frontend application on one server!

## Single Deploy (docker-compose)
To deploy `EasyVPN` on a single server, use `Docker Compose`:

### Production enviroment:

```bash
curl -L -o docker-compose.yml https://raw.githubusercontent.com/F1st3K/EasyVPN/refs/heads/main/docker-compose.yml && \
curl -L -o .env https://raw.githubusercontent.com/F1st3K/EasyVPN/refs/heads/main/.env.dev
```

> It is recommended to change variables in `.env`

```bash
docker-compose --profile init up
```

> `--profile init` - used at the first run to initialize data, then just run:
```bash
docker-compose up
```

### Develop environment:

```bash
git clone https://github.com/F1st3K/EasyVPN
```

```bash
docker-compose --env-file .env.dev --profile init up --build
```

> `--profile init` - used at the first launch to initialize data, then just build and launch:
```bash
docker-compose --env-file .env.dev up --build
```

Also for independent deployment of the components of the `EasyVPN` web application, you can use the documentation for each service:
[EasyVPN backend](backend/README.md), [EasyVPN frontend](/frontend/README.md), [EasyVPN init](/init/README.md), as well as others [EasyVPN services](/services/README.md).