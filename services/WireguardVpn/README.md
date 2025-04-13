[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


# Service WireguardVpn
Сервисное приложение для работы с поключениями WireGuard имеющее простое http-api. (Golang + gin)


## Configuration
> Конфигурация сервиса находится в [config.yaml](./cmd/config.yaml):

`api: port: "8000"` - порт на котором будет работать http-api (либо через env переменную API_PORT)

`vpn: port: "51820"` - порт через который устанавливается WireGuard соединение (либо через env переменную VPN_PORT)

> Так же дополнительно для работы сервиса необходима enviroment переменные, перечечисленные в [.env.example](./cmd/.env.example):

`SERVICE_HOST` - адрес хоста на котором будет запущен сервис

`SERVICE_USER` и `SERVICE_PASSWORD` - имя пользователя и пароль для basic auth к http-api


## Deploy (docker)
Для запуска сервиса в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name wireguard-vpn-service \
  -e SERVICE_HOST=89.191.226.158 `#your host address` \
  -e SERVICE_USER=user `#your user name for auth` \
  -e SERVICE_PASSWORD=passwd `#your password for auth` \
  -p 51820:51820/udp `#wireguard port` \
  -p 8000:8000/tcp `#http-api port` \
  -v ~/.WireguardVpn:/etc/wireguard \
  --cap-add=NET_ADMIN \
  --cap-add=SYS_MODULE \
  --sysctl="net.ipv4.conf.all.src_valid_mark=1" \
  --sysctl="net.ipv4.ip_forward=1" \
  --restart unless-stopped \
  easyvpn/wireguard-vpn:latest
```