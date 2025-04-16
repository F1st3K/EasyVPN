[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

# EasyVPN services

### This section contains VPN, as well as other separate project services.

## Other services:

### Unrelated project services:

1. **TelegramBot - `bot-service`** - telegram bot on **Python**, for more convenient interaction and notification of users.

## VPN services

### Services managing their VPN connections, having a common versioned `vpn-api`:

1. **WireguardVpn - `wireguard-vpn-service`** - VPN service on **GO**, working on the **WireGuard** protocol.

2. **AmneziaWgVpn - `amneziawg-vpn-service`** - VPN service on **GO**, working on the **AmneziaWG** protocol (improved WireGuard).

### Table of `vpn-api` versions support:
| ***🧩 Service / Version*** | **`V1`** |
|--------------------------|:--------:|
| `wireguard-vpn-service` | ✅ |
| `amneziawg-vpn-service` | ✅ |

-------------------------------------------------------------------

## VPN-API `V1`

### Authorization of actions
> All endpoints to the service api are available only via Basic Authorization
```http
GET http://{{host}}/v1/
Authorization: Basic {{Username}} {{Password}}
```

### Test endpoint
```http
GET http://{{host}}/v1/
Authorization: Basic {{Username}} {{Password}}
```
#### response:
```http
200 OK
```

### Getting connection configuration
```http
GET http://{{host}}/v1/connections/{{connectionId}}/config
Authorization: Basic {{Username}} {{Password}}
```
#### response:
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

### Creating (inactive) connection
```http
POST http://{{host}}/v1/connections?id={{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### answer:
```http
201 Created
```

### Activate the connection
```http
PUT http://{{host}}/v1/connections/{{connectionId}}/enable
Authorization: Basic {{Username}} {{Password}}
```
#### answer:
```http
204 No content
```

### Deactivate a connection
```http
PUT http://{{host}}/v1/connections/{{connectionId}}/disable
Authorization: Basic {{Username}} {{Password}}
```
#### response:
```http
204 No content
```

### Deleting connection
```http
DELETE http://{{host}}/v1/connections/{{connectionId}}
Authorization: Basic {{Username}} {{Password}}
```
#### response:
```http
204 No content
```