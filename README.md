[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


<div align="center">

![Logo](.img/EasyZsV-logo.png)

# EasyZsV

[![GitHub Release](https://img.shields.io/github/v/release/F1st3K/EasyZsV)](https://hub.docker.com/u/easyzsv)

[![Build](https://github.com/F1st3K/EasyZsV/actions/workflows/test-single-deploy.yml/badge.svg)](https://github.com/F1st3K/EasyZsV/actions/workflows/test-single-deploy.yml) 
[![DEV](https://img.shields.io/badge/dynamic/json?label=DEV&url=https%3A%2F%2Fdev.easy-zsv.f1st3k.tw1.su%2Fapi%2Fhealth&query=%24.version&color=yellowgreen)](https://dev.easy-zsv.f1st3k.tw1.su)
[![PROD](https://img.shields.io/badge/dynamic/json?label=PROD&url=https%3A%2F%2Feasy-zsv.f1st3k.tw1.su%2Fapi%2Fhealth&query=%24.version&color=brightgreen)](https://easy-zsv.f1st3k.tw1.su)



[![License: CC BY-NC 4.0](https://img.shields.io/badge/License-CC_BY--NC_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc/4.0/)
[![GitHub contributors](https://img.shields.io/github/contributors/F1st3K/EasyZsV)](https://GitHub.com/F1st3K/EasyZsV/graphs/contributors/) 
[![GitHub Stars](https://img.shields.io/github/stars/F1st3K/EasyZsV.svg)](https://github.com/F1st3K/EasyZsV/stargazers) 


EasyZsV — Защищённые cкоростные Волны? Да Easy!

[![System Design](.img/SystemDesign.excalidraw.svg)**💻 Watch**](https://www.youtube.com/watch?v=nR8FZ8_98pk)

---

</div>

## Single Deploy (docker-compose)
Для развертывания `EasyZsV` на одном единственном сервере используйте `Docker Compose` :

### Production enviroment:

```bash
curl -L -o docker-compose.yml https://raw.githubusercontent.com/F1st3K/EasyZsV/refs/heads/main/docker-compose.yml && \
curl -L -o .env https://raw.githubusercontent.com/F1st3K/EasyZsV/refs/heads/main/.env.dev
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
git clone https://github.com/F1st3K/EasyZsV
```

```bash
docker-compose --env-file .env.dev --profile init up --build
```

> `--profile init` -  используется при первом запуске для инициализации данных, далее просто сборка и запуск:
```bash
docker-compose --env-file .env.dev up --build
```

Так же для независимого развертывания компонентов web-приложения `EasyZsV`, можно воспользоваться документацией для каждого сервиса:
[EasyZsV backend](backend/README.md), [EasyZsV frontend](/frontend/README.md), [EasyZsV init](/init/README.md), а так же остальные [EasyZsV services](/services/README.md).
