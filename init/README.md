[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md

# EasyVPN init
Серивис инициализации данных, очень простой и быстрый, который лазит в api и db (bash + SQL)

## Configuration
> Для запускао сервиса необходимы enviroment переменные, перечечисленные в [.env.example](./cmd/.env.example):

`API` - URL до запущенного бекенда. 

`DB_CONNECTION_STRING` - строка подключения к главной БД.

`CREATE_SECURITY_KEEPER` - необязательна - переменная с логином:паролем создающаяя пользоавтеля с выдачей прав, пользователь не создается если переменная указана.

## Run (docker):
Для запуска инициализации в докер-контейнере используйте следующую команду:

```sh
docker run -d \
  --name init \
  -e API=http//localhost:80/api \
  -e DB_CONNECTION_STRING=postgresql://postgres:mysecretpassword@localhost:5432 \
  -e CREATE_SECURITY_KEEPER=admin:admin \
  easyvpn/init
```