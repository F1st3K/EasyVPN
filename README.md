# EasyVPN
Простое VPN + Backend + Frontend приложения на одном сервере!

## Deploy
Для развертывания `EasyVPN` на одном единственном сервере используйте `Docker Compose` :
```bash
docker-compose up --build
```
> `Важно:` При таком варианте развертывания убедитесь что в frontend-конфигурации [config.json](./frontend/src/config.json), указано `"ApiUrl": "api"` для правильного перенаправления запросов к api.

Так же для независимого развертывания компонентов web-приложения `EasyVPN`, можно воспользоваться `Docker-контейнерами` :

##### `Для Postgres DB:`
```bash
docker run -d \
  --name database \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=mysecretpassword \
  -p 5432:5432 \
  -v ~/postgresql/:/var/lib/postgresql/data \
  postgres:latest
```

##### `Для Backend API:`
```bash
docker build -t easyvpn/backend:local ./backend/ && \
docker run -d \
  --name backend \
  -p 80:80 \
  easyvpn/backend:local
```

##### `Для Frontend WEB-UI:`
```bash
docker build -t easyvpn/frontend:local ./frontend/ && \
docker run -d \
  --name frontend \
  -p 3000:3000 \
  easyvpn/frontend:local
```
> Помните о конфигурациях [`appsettings.json`](./backend/src/EasyVPN.Api/appsettings.json) и [`config.json`](./frontend/src/config.json)

## Backend API

### Ошибки:

#### Конечная точка ошибок
```http
{{host}}/error
```

#### Ответ ошибки
```http
400 Bad Request
``` 
```http
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Invalid login or password",
  "status": 400,
  "traceId": "00-ddbb5dae1dcf8a772b1e236a7259f07e-e58fef179b5dd0fb-00",
  "errorCodes": [
    "Authentication.InvalidCredentials"
  ]
}
```

### Авторизация:

##### Запрос регистрации
```http
POST {{host}}/auth/register
Content-Type: application/json
{
    "firstName": "Freak",
    "lastName": "Fister",
    "login": "F1st3K",
    "password": "fisty123"
}
```

##### Запрос входа
```http
POST {{host}}/auth/login
Content-Type: application/json

{
    "login": "F1st3K",
    "password": "fisty123"
}
```

##### Запрос проверки токена
```http
GET {{host}}/auth/check
Authorization: Bearer {{token}}
```

##### Ответ авторизации
```http
200 OK
```
```http
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cC...GuPiBb8xTszyr8V60WKzNlk",
  "id": "7673b7c8-e583-4e28-aa6a-11e06822a48c",
  "firstName": "Freak",
  "lastName": "God",
  "login": "god",
  "roles": [
    "Administrator",
    "PaymentReviewer",
    "Client"
  ]
}
```
### VPN действия клиента:

#### Создание нового подключения
```http
POST {{host}}/my/connections
Authorization: Bearer {{clientToken}}
Content-Type: application/json

{
    "serverId": "00000000-0000-0000-0000-000000000000",
    "Days": 30,
    "Description": "I am payment this",
    "Images" : [
      "image1",
      "image2",
      "image3"
    ]
}
```
##### Ответ
```http
200 OK
```

#### Создание заявки на продление подключения
```http
POST {{host}}/my/connections/extend
Authorization: Bearer {{clientToken}}
Content-Type: application/json

{
    "connectionId": "00000001-0000-0000-0000-000000000000",
    "Days": 30,
    "Description": "I am payment this",
    "Images": [
      "image1",
      "image2",
      "image3"
    ]
}
```
##### Ответ
```http
200 OK
```

#### Удаление истекшего подключения
```http
DELETE {{host}}/my/connections/{{connectionId}}
Authorization: Bearer {{clientToken}}
```
##### Ответ
```http
200 OK
```

#### Получение всех подключений
```http
GET {{host}}/my/connections
Authorization: Bearer {{clientToken}}
```
##### Ответ
```http
200 OK
```
```http
[
  {
    "id": "1b116808-90d7-4618-8ab9-cdeaa479f1de",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "server": {
      "id": "00000000-0000-0000-0000-000000000000",
      "protocolResponse": {
        "id": "00000000-0000-0000-0000-000000000000",
        "name": "WireGuard",
        "icon": "data:image/svg+xml;base64,PHN2ZyB4...MDQxLS4zNjkuMDQxIi8+PC9zdmc+"
      },
      "version": "HttpV1"
    },
    "validUntil": "2024-04-24T09:13:16.828142Z"
  },
  {
    "id": "4dc25868-48fc-45c0-b469-4e4626fab087",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "server": {
      "id": "00000000-0000-0000-0000-000000000000",
      "protocolResponse": {
        "id": "00000000-0000-0000-0000-000000000000",
        "name": "WireGuard",
        "icon": "data:image/svg+xml;base64,PHN2ZyB4...MDQxLS4zNjkuMDQxIi8+PC9zdmc+"
      },
      "version": "HttpV1"
    },
    "validUntil": "2024-04-24T09:13:26.732671Z"
  }
]
```

#### Получение всех заявок подключений
```http
GET {{host}}/my/tickets
Authorization: Bearer {{clientToken}}
```
##### Ответ
```http
200 OK
```
```http
[
  {
    "id": "cb63d996-b46c-4403-bc3e-bd66ff2b827e",
    "connectionId": "4dc25868-48fc-45c0-b469-4e4626fab087",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "status": "Rejected",
    "creationTime": "2024-04-24T09:13:28.732586Z",
    "days": 30,
    "paymentDescription": "I am payment this",
    "images": [
      "image1",
      "image2",
      "image3"
    ]
  },
  {
    "id": "9529b5bf-28d4-48d8-93ec-192e88266ad5",
    "connectionId": "0d83108d-e9ed-4bc0-80c1-456146c982e0",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "status": "Confirmed",
    "creationTime": "2024-04-24T15:21:53.548129Z",
    "days": 30,
    "paymentDescription": "I am payment this",
    "images": [
      "image1",
      "image2",
      "image3"
    ]
  }
]
```

#### Получение конфигурации для подключения
```http
GET {{host}}/my/connections/{{connectionId}}/config
Authorization: Bearer {{clientToken}}
```
##### Ответ
```http
200 OK
```
```http
{
  "clientId": "b145847c-944b-469d-b07c-3c2555c5180f",
  "config": "\n[Interface]\nPrivateKey = ABwSjmQW+D8zjJqpn4sQLmKWvBSGtFWWbmTtqp+MlV0=\nAddress = 10.0.0.36/32\nDNS = 1.1.1.1\n\n[Peer]\nPublicKey = eiHywAHmOLFrMvmfAU/tyrV4lpt9D7OQpp0QlVPsPy8=\n\nEndpoint = 89.191.226.158:51820\nAllowedIPs = 0.0.0.0/0\n"
}
```


### VPN действия администратора:

#### Создание нового подключения
```http
POST {{host}}/client/{{clientId}}/connections?serverId={{serverId}}
Authorization: Bearer {{adminToken}}
```
##### Ответ
```http
200 OK
```

#### Сброс времени жизни подключения
```http
PUT {{host}}/connections/{{connectionId}}/reset
Authorization: Bearer {{adminToken}}
```
##### Ответ
```http
200 OK
```

#### Получение всех подключений
```http
GET {{host}}/connections
Authorization: Bearer {{adminToken}}
```
#### Получение подключений клиента
```http
GET {{host}}/connections?clientId={{clientId}}
Authorization: Bearer {{adminToken}}
```
##### Ответ
```http
200 OK
```
```http
[
  {
    "id": "35274436-5de6-4f03-b0bd-d8c64d9d6afc",
    "client": {
      "id": "7673b7c8-e583-4e28-aa6a-11e06822a48c",
      "firstName": "Freak",
      "lastName": "God",
      "login": "god",
      "roles": [
        "Administrator",
        "PaymentReviewer",
        "Client"
      ]
    },
    "server": {
      "id": "00000000-0000-0000-0000-000000000000",
      "protocolResponse": {
        "id": "00000000-0000-0000-0000-000000000000",
        "name": "WireGuard",
        "icon": "data:image/svg+xml;base64,PHN2ZyB4...MDQxLS4zNjkuMDQxIi8+PC9zdmc+"
      },
      "version": "HttpV1"
    },
    "validUntil": "2024-04-24T15:22:33.617239Z"
  },
  {
    "id": "0d83108d-e9ed-4bc0-80c1-456146c982e0",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "server": {
      "id": "00000000-0000-0000-0000-000000000000",
      "protocolResponse": {
        "id": "00000000-0000-0000-0000-000000000000",
        "name": "WireGuard",
        "icon": "data:image/svg+xml;base64,PHN2ZyB4...MDQxLS4zNjkuMDQxIi8+PC9zdmc+"
      },
      "version": "HttpV1"
    },
    "validUntil": "2024-04-24T15:26:00.380088Z"
  }
]
```


#### Получение конфигурации для подключения
```http
GET {{host}}/connections/{{connectionId}}/config
Authorization: Bearer {{adminToken}}
```

##### Ответ
```http
200 OK
```
```http
{
  "clientId": "b145847c-944b-469d-b07c-3c2555c5180f",
  "config": "\n[Interface]\nPrivateKey = OLKznNRtkpu8zStcE0V+/LNXf0pOt8yF0V9nPoMZxEA=\nAddress = 10.0.0.39/32\nDNS = 1.1.1.1\n\n[Peer]\nPublicKey = eiHywAHmOLFrMvmfAU/tyrV4lpt9D7OQpp0QlVPsPy8=\n\nEndpoint = 89.191.226.158:51820\nAllowedIPs = 0.0.0.0/0\n"
}
```


### VPN действия проверяющего платежи:

#### Подтвердить платеж по заявке подключения
```http
PUT {{host}}/payment/tickets/{{connectionTicketId}}/confirm
Authorization: Bearer {{PaymentToken}}
```
##### Ответ
```http
200 OK
```

#### Отклонить заявку оплаты на подключение
```http
PUT {{host}}/payment/tickets/{{connectionTicketId}}/reject
Authorization: Bearer {{PaymentToken}}
```
##### Ответ
```http
200 OK
```

#### Получение всех заявок на подключение
```http
GET {{host}}/payment/tickets
Authorization: Bearer {{PaymentToken}}
```
#### Получение всех заявок на подключение клиента
```http
GET {{host}}/payment/tickets?clientId={{clientId}}
Authorization: Bearer {{PaymentToken}}
```
##### Ответ
```http
200 OK
```
```http
[
  {
    "id": "29c8b492-883e-4eb0-a829-9788ad07fd4e",
    "connectionId": "35274436-5de6-4f03-b0bd-d8c64d9d6afc",
    "client": {
      "id": "7673b7c8-e583-4e28-aa6a-11e06822a48c",
      "firstName": "Freak",
      "lastName": "God",
      "login": "god",
      "roles": [
        "Administrator",
        "PaymentReviewer",
        "Client"
      ]
    },
    "status": "Rejected",
    "creationTime": "2024-04-24T15:22:33.978444Z",
    "days": 30,
    "paymentDescription": "I am payment this",
    "images": [
      "image1",
      "image2",
      "image3"
    ]
  },
  {
    "id": "9529b5bf-28d4-48d8-93ec-192e88266ad5",
    "connectionId": "0d83108d-e9ed-4bc0-80c1-456146c982e0",
    "client": {
      "id": "b145847c-944b-469d-b07c-3c2555c5180f",
      "firstName": "Freak",
      "lastName": "Client",
      "login": "client",
      "roles": [
        "Client"
      ]
    },
    "status": "Confirmed",
    "creationTime": "2024-04-24T15:21:53.548129Z",
    "days": 30,
    "paymentDescription": "I am payment this",
    "images": [
      "image1",
      "image2",
      "image3"
    ]
  }
]
```

---



<details>
	<summary>Логика приложения</summary>

### Для всех
![Для всех](img/logicMap/anyone.jpg)

### Для авторизованых
![Для авторизованых](img/logicMap/any_auth.jpg)

### Для клиентов
![Для клиентов](img/logicMap/client.jpg)

### Для проверяющего оплату
![Для проверяющего оплату](img/logicMap/payment_reviewer.jpg)

### Для администраторов
![Для администраторов](img/logicMap/administrator.jpg)

</details>

---
<details>
	<summary>Прототип будущего сайта</summary>


### Главная страница
![Главная страница](img/prototype/main.jpg)

### Страница авторизации
![Страница авторизации](img/prototype/sign_in.jpg)

### Страница регистрации
![Страница регистрации](img/prototype/sign_up.jpg)

### Профиль обычного пользователя
![Профиль обычного пользователя](img/prototype/user_profile.jpg)

### Профиль администратора
![Профиль администратора](img/prototype/admin_profile.jpg)

### Страница заявок на подключение
![Страница заявок на подключение](img/prototype/connection_tickets.jpg)

### Страница заявок потдержки
![Страница заявок потдержки](img/prototype/support_tickets.jpg)

### Страница администрирования пользователей
![Страница администрирования пользователей](img/prototype/administrate_users.jpg)

</details>

-----------------------------------------------------