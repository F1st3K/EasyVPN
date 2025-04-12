# EasyVPN services 

### В этом разделе находятся VPN, а также другие отделенные сервисы проекта.


## Other services:

### Не связанные сервисы проекта:

1. **TelegramBot - `bot-service`** - телеграм бот на **Python**, для более удобного взаимодействия и нотификации пользователей.


## VPN services

### Сервисы управляющие своими vpn соеденениями, имеющие общее версионируемое `vpn-api`:

1. **WireguardVpn - `wireguard-vpn-service`** - vpn-сервис на **GO**, работающий на протаколе **WireGuard**.

2. **AmneziaWgVpn - `amneziawg-vpn-service`** - vpn-сервис на **GO**, работающий на протаколе **AmneziaWG** (улучшенный WireGuard).


### Таблица потдержки версий `vpn-api`:
| ***🧩 Сервис / Версия*** | **`V1`** |
|--------------------------|:--------:|
| `wireguard-vpn-service`  |    ✅    |
| `amneziawg-vpn-service`  |    ✅    |

-------------------------------------------------------------------


## VPN-API `V1`

### Авторизация действий
> Все конечные точки к api сервиса доступны только по Basic Authorization
```http
GET http://{{host}}/v1/
Authorization: Basic {{Username}} {{Password}}
```

### Тестовая конечная точка
```http
GET http://{{host}}/v1/
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
200 OK
```

### Получение конфигурации для подключения
```http
GET http://{{host}}/v1/connections/{{connectionId}}/config
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
POST http://{{host}}/v1/connections?id={{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
201 Created
```

### Активация подключения
```http
PUT http://{{host}}/v1/connections/{{connectionId}}/enable
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```

### Деактивация подключения
```http
PUT http://{{host}}/v1/connections/{{connectionId}}/disable
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```

### Удаление подключения
```http
DELETE http://{{host}}/v1/connections/{{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### ответ:
```http
204 No content
```