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
  "id": "a125f63a-5919-4e91-ab80-625b75ef76c8",
  "firstName": "Freak",
  "lastName": "Fister",
  "login": "F1st3K",
  "roles": [
    "Client"
  ],
  "token": "eyJhbGciOiJ...DnzD4pvOcVo"
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
    "Description": "I am payment this"
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
    "connectionId": "00000000-0000-0000-0000-000000000000",
    "Days": 30,
    "Description": "I am payment this"
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
    "id": "00000001-0000-0000-0000-000000000000",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "serverId": "00000000-0000-0000-0000-000000000000",
    "validUntil": "0001-01-01T00:00:00"
  },
  {
    "id": "00000002-0000-0000-0000-000000000000",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "serverId": "00000000-0000-0000-0000-000000000000",
    "validUntil": "9999-12-31T23:59:59.9999999"
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
    "id": "c0e8b84f-4314-41bb-ac31-70c46114495d",
    "connectionId": "c21f18d2-2c52-4422-8e6d-5ce4d075337e",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "status": "Pending",
    "creationTime": "2024-03-19T14:20:21.7572023Z",
    "days": 30,
    "paymentDescription": "I am payment this"
  },
  {
    "id": "0fb632e3-0bbd-411c-9da9-815e30579dc9",
    "connectionId": "c2c96b40-1276-4209-b477-39e6b1e8eb3f",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "status": "Pending",
    "creationTime": "2024-03-19T14:20:22.2159667Z",
    "days": 30,
    "paymentDescription": "I am payment this"
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
  "clientId": "00000001-0000-0000-0000-000000000000",
  "config": "password=qwertyi1234567"
}
```


### VPN действия администратора:

#### Создание нового подключения
```http
POST {{host}}/client/{{clientId}}/connections
Authorization: Bearer {{adminToken}}
Content-Type: application/json

{
  "serverId": "00000000-0000-0000-0000-000000000000",
  "CountDays": 30
}
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
    "id": "00000001-0000-0000-0000-000000000000",
    "clientId": "00000002-0000-0000-0000-000000000000",
    "serverId": "00000000-0000-0000-0000-000000000000",
    "validUntil": "0001-01-01T00:00:00"
  },
  {
    "id": "00000002-0000-0000-0000-000000000000",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "serverId": "00000000-0000-0000-0000-000000000000",
    "validUntil": "9999-12-31T23:59:59.9999999"
  },
  {
    "id": "c21f18d2-2c52-4422-8e6d-5ce4d075337e",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "serverId": "00000000-0000-0000-0000-000000000000",
    "validUntil": "2024-03-19T14:20:21.7442087Z"
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
  "clientId": "00000001-0000-0000-0000-000000000000",
  "config": "password=qwertyi1234567"
}
```


### VPN действия проверяющего платежи:

#### Подтвердить платеж по заявке подключения
```http
PUT {{host}}/payment/{{connectionId}}/confirm
Authorization: Bearer {{PaymentToken}}
```
##### Ответ
```http
200 OK
```

#### Отклонить заявку оплаты на подключение
```http
PUT {{host}}/payment/{{connectionId}}/reject
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
    "id": "c0e8b84f-4314-41bb-ac31-70c46114495d",
    "connectionId": "c21f18d2-2c52-4422-8e6d-5ce4d075337e",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "status": "Pending",
    "creationTime": "2024-03-19T14:20:21.7572023Z",
    "days": 30,
    "paymentDescription": "I am payment this"
  },
  {
    "id": "0fb632e3-0bbd-411c-9da9-815e30579dc9",
    "connectionId": "c2c96b40-1276-4209-b477-39e6b1e8eb3f",
    "clientId": "00000001-0000-0000-0000-000000000000",
    "status": "Pending",
    "creationTime": "2024-03-19T14:20:22.2159667Z",
    "days": 30,
    "paymentDescription": "I am payment this"
  },
  {
    "id": "f461d641-0ac0-49fe-b9e4-f6e44e2dd58e",
    "connectionId": "b63e2180-3dff-4b28-be31-a127a233ad05",
    "clientId": "a9ba4de4-de78-46e4-bcb4-e024a4128d7a",
    "status": "Pending",
    "creationTime": "2024-03-19T14:29:11.6537823Z",
    "days": 30,
    "paymentDescription": "I am payment this"
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