# Service WireguardVpn
Сервисное приложение для работы с поключениями WireGuard имеющее простое http-api.

## WireguardVpn API

### Авторизация действий
> Все конечные точки к api сервиса доступны только по Basic Authorization
```http
GET http://{{host}}/
Authorization: Basic {{Username}} {{Password}}
```
> Username и Password задаются в конфигурации сервиса: [config.yaml](./cmd/config.yaml)

### Тестовая конечная точка
```http
GET http://{{host}}/
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
200 OK
```

### Получение конфигурации для подключения
```http
GET http://{{host}}/connections/{{connectionId}}/config
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
200 OK
```
```
[Interface]
PrivateKey = eH1jlKve73U2ZkbYJJZA1cBkvj4zqDZU75YskzwCXE8=
Address = 10.0.0.2/32
DNS = 1.1.1.1

[Peer]
PublicKey = s1WUppCKxNYYPra/gkPTz/LQrUDJbfqCpI37T92N5F0=
Endpoint = 89.191.226.158:51840
AllowedIPs = 0.0.0.0/0
```

### Создание (неактивного) подключения
```http
POST http://{{host}}/connections?id={{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
201 Created
```

### Активация подключения
```http
PUT http://{{host}}/connections/{{connectionId}}/enable
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```

### Деактивация подключения
```http
PUT http://{{host}}/connections/{{connectionId}}/disable
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```

### Удаление подключения
```http
DELETE http://{{host}}/connections/{{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```


## Configuration
Конфигурация сервиса находится в [config.yaml](./cmd/config.yaml):

`api_port: "8000"` - порт на котором будет работать http-api

`wg_port: "51820"` - порт через который устанавливается WireGuard соединение

`user: "user"` и `password: "passwd"` - логин и пароль для доступа к запросам http-api

> Так же дополнительно для работы сервиса необходима enviroment переменная $HOST в которую необходимо поместить ip-адресс по которому и будет доступен сервис:
`$HOST=192.0.0.1` или `$HOST=localhost`


## Deploy (docker)
Сборка докер-образа:
```bash
docker build -t wireguardvpn:local ./
```
Для запуска сервиса в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  -e HOST=89.191.226.158 `#your server ip` \
  -p 51840:51820/udp `#wireguard port` \
  -p 8000:8000/tcp `#http-api port` \
  -v ~/.WireguardVpn:/etc/wireguard \
  --cap-add=NET_ADMIN \
  --cap-add=SYS_MODULE \
  --sysctl="net.ipv4.conf.all.src_valid_mark=1" \
  --sysctl="net.ipv4.ip_forward=1" \
  --restart unless-stopped \
  wireguardvpn:local
```