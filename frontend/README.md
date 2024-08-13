# EasyVPN frontend
Приложение web-интерфейс написаный на React + TypeScript.


## Configuration
> Конфигурация web-интерфейса находится в [config.json](./src/config.json):

`"ApiUrl": "http://localhost:80/api/",` - URL к развернотому [EasyVPN backend](../backend/README.md), с которым общается web-интерфейс

`"AuthCheckMinutes": 15` - переодичность в минутах, с которой автоматически проверяется авторизация
(так же авторизация автоматически проверяется при изменении навигации)


## Deploy (docker)
Сборка докер-образа:
```bash
docker build -t easyvpn/frontend:local ./
```

Для запуска сервиса в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name frontend \
  -p 3000:3000 `#your port` \
  easyvpn/frontend:local
```

