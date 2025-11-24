[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


# EasyZsV services 

### В этом разделе находятся ZSV, а также другие отделенные сервисы проекта.


## Other services:

### Не связанные сервисы проекта:

1. **TelegramBot - `bot-service`** - телеграм бот на **Python**, для более удобного взаимодействия и нотификации пользователей.


## ZSV services

### Сервисы управляющие своими zsv соеденениями, имеющие общее версионируемое `zsv-api`:

1. **WireguardZsv - `wireguard-zsv-service`** - zsv-сервис на **GO**, работающий на протоколе **WireGuard**.

2. **AmneziaWgZsv - `amneziawg-zsv-service`** - zsv-сервис на **GO**, работающий на протоколе **AmneziaWG** (улучшенный WireGuard).


### Таблица потдержки версий `zsv-api`:
| ***🧩 Сервис / Версия*** | **`V1`** |
|--------------------------|:--------:|
| `wireguard-zsv-service`  |    ✅    |
| `amneziawg-zsv-service`  |    ✅    |

-------------------------------------------------------------------


## ZSV-API `V1`

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