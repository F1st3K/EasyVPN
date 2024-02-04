[![en](https://img.shields.io/badge/lang-English%20%F0%9F%87%AC%F0%9F%87%A7-white)](README-EN.md)
[![ru](https://img.shields.io/badge/%D1%8F%D0%B7%D1%8B%D0%BA-%D0%A0%D1%83%D1%81%D1%81%D0%BA%D0%B8%D0%B9%20%F0%9F%87%B7%F0%9F%87%BA-white)](README.md)

# EasyVPN

Simple VPN + Backend + Frontend application on a single server!

<details>
	<summary><h2>EasyVPN API</h2></summary>

### Errors

#### Errors endpoint
```http
{{host}}/error
```

#### Errors response
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

### Authentication

##### Register request
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

##### Login request

```http
POST {{host}}/auth/login
Content-Type: application/json

{
    "login": "F1st3K",
    "password": "fisty123"
}
```

##### Auth response
```http
200 OK
```
```http
{
    "id":"88755e3c-e106-4283-bf93-17965b1a"
    "firstName": "Freak",
    "lastName": "Fister",
    "login": "F1st3K",
    "token":"56be52...e3c7743d"
}
```

</details>

---

<details>
	<summary><h2>Prototype of the future site</h2></summary>

### Main page

![Main page](img/prototype/main.jpg)

### Authorization page

![Authorization page](img/prototype/sign_in.jpg)

### Registration page

![Registration page](img/prototype/sign_up.jpg)

### User profile

![User profile](img/prototype/user_profile.jpg)

### Admin profile

![Admin profile](img/prototype/admin_profile.jpg)

### Connection requests page

![Connection requests page](img/prototype/connection_tickets.jpg)

### Support requests page

![Support requests page](img/prototype/support_tickets.jpg)

### User administration page

![User administration page](img/prototype/administrate_users.jpg)

</details>

---
