# Service AmneziaWgVpn
Сервисное приложение для работы с поключениями AmneziaWG имеющее простое http-api. (Golang + gin)


## Configuration
> Конфигурация сервиса находится в [config.yaml](./cmd/config.yaml):

`api: port: "8010"` - порт на котором будет работать http-api (либо через env переменную API_PORT)

`wg: port: "51840"` - порт через который устанавливается Amnezia WireGuard соединение (либо через env переменную VPN_PORT)

> Так же дополнительно для работы сервиса необходима enviroment переменные, перечечисленные в [.env.example](./cmd/.env.example):

`SERVICE_HOST` - адрес хоста на котором будет запущен сервис

`SERVICE_USER` и `SERVICE_PASSWORD` - имя пользователя и пароль для basic auth к http-api


## Deploy (docker)
Для запуска сервиса в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name amneziawg-vpn-service \
  -e SERVICE_HOST=89.191.226.158 `#your host address` \
  -e SERVICE_USER=user `#your user name for auth` \
  -e SERVICE_PASSWORD=passwd `#your password for auth` \
  -p 51840:51840/udp `# amnezia wg port` \
  -p 8010:8010/tcp `#http-api port` \
  -v ~/.AmneziaWgVpn:/etc/amnezia \
  --cap-add=NET_ADMIN \
  --cap-add=SYS_MODULE \
  --sysctl="net.ipv4.conf.all.src_valid_mark=1" \
  --sysctl="net.ipv4.ip_forward=1" \
  --device /dev/net/tun:/dev/net/tun \
  --restart unless-stopped \
  easyvpn/amneziawg-vpn:latest
```