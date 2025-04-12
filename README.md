# EasyVPN
Простое VPN + Backend + Frontend приложение на одном сервере!

## Single Deploy (docker-compose)
Для развертывания `EasyVPN` на одном единственном сервере используйте `Docker Compose` :

### Production enviroment:

```bash
curl -L -o docker-compose.yml https://raw.githubusercontent.com/F1st3K/EasyVPN/refs/heads/main/docker-compose.yml && \
curl -L -o .env https://raw.githubusercontent.com/F1st3K/EasyVPN/refs/heads/main/.env.dev
```

> Рекомендуется изменить переменные в `.env`

```bash
docker-compose --profile init up
```

> `--profile init` -  используется при первом запуске для инициализации данных, далее просто запуск:
```bash
docker-compose up
```

### Develop enviroment:

```bash
git clone https://github.com/F1st3K/EasyVPN
```

```bash
docker-compose --env-file .env.dev --profile init up --build
```

> `--profile init` -  используется при первом запуске для инициализации данных, далее просто сборка и запуск:
```bash
docker-compose --env-file .env.dev up --build
```

Так же для независимого развертывания компонентов web-приложения `EasyVPN`, можно воспользоваться документацией для каждого сервиса:
[EasyVPN backend](backend/README.md), [EasyVPN frontend](/frontend/README.md), [EasyVPN init](/init/README.md), а так же остальные [EasyVPN services](/services/README.md).

