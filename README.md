[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


<div align="center">

![Logo](.img/EasyVPN-logo.png)

# EasyVPN

[![Build](https://github.com/F1st3K/EasyVPN/actions/workflows/test-single-deploy.yml/badge.svg)](https://github.com/F1st3K/EasyVPN/actions/workflows/test-single-deploy.yml) 

[![GitHub contributors](https://img.shields.io/github/contributors/F1st3K/EasyVPN)](https://GitHub.com/F1st3K/EasyVPN/graphs/contributors/) 
[![GitHub Stars](https://img.shields.io/github/stars/F1st3K/EasyVPN.svg)](https://github.com/F1st3K/EasyVPN/stargazers) 
[![License: CC BY-NC 4.0](https://img.shields.io/badge/License-CC_BY--NC_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc/4.0/)
[![GitHub Release](https://img.shields.io/github/v/release/F1st3K/EasyVPN)](https://hub.docker.com/u/easyvpn)

Простое VPN + Backend + Frontend приложение на одном сервере!

[![System Design](.img/SystemDesign.excalidraw.svg)](https://www.youtube.com/watch?v=nR8FZ8_98pk)

---

</div>

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