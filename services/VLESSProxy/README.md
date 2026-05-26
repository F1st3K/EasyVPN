[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


# Service Vless proxy
Сервисное приложение для работы с поключениями Vless+Reality имеющее простое http-api. (Golang + gin)


## Configuration
`api: port: "8030"` - порт на котором будет работать http-api (либо через env переменную API_PORT)

`wg: port: "444"` - порт через который устанавливается Vless соединение (либо через env переменную SING_BOX_PORT)

> Так же дополнительно для работы сервиса необходимы enviroment переменные, перечечисленные в [.env.template](.env.template):

`SERVICE_HOST` - адрес хоста на котором будет запущен сервис

`SERVICE_USER` и `SERVICE_PASSWORD` - имя пользователя и пароль для basic auth к http-api


## Deploy (docker)
Для запуска сервиса в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  -p 8030:8030 \
  -p 444:444 \
  --env-file \
  .env \  #если пользуетесь .env
  -v \ 
  $(pwd)/data:/app/data vlessproxy
```