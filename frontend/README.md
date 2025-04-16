[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md

# EasyVPN frontend

Приложение веб-интерфейс, имеющее простой UI. (TypeScript + React)

## Configuration

> Конфигурация web-интерфейса находится в [`config.ts`](./src/config.ts), подменяется посредством enviroment переменных:

`REACT_APP_API_URL=https://localhost:80/api/` - URL к развернотому [`EasyVPN backend`](../backend/README.md), с которым общается веб-интерфейс

`REACT_APP_AUTH_CHECK_MINUTES=15` - переодичность в минутах, с которой автоматически проверяется авторизация
(так же авторизация автоматически проверяется при изменении навигации)

## Deploy (docker)

Для запуска сервиса в докер-контейнере используйте следующую команду:

```bash
docker run -d \
  --name frontend \
  -p 3000:3000 `#your port` \
  -e REACT_APP_API_URL=https://localhost:80/api/ \
  -e REACT_APP_AUTH_CHECK_MINUTES=15 \
  easyvpn/frontend:latest
```
