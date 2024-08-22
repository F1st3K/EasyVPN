[![readme-en-shield]][readme-en-url]
[![readme-ru-shield]][readme-ru-url]

# EasyVPN
EasyVPN is a self-hosted VPN connection management and control service that simplifies 
the process of VPN network administration. The project includes a server and client 
part, providing the ability to centrally manage access to the VPN on your own server.

## Features

* VPN connection control: EasyVPN allows administrators to monitor active VPN sessions,
  manage users and their access to network resources.
* Connection requests: Users can send VPN connection requests through the system. Administrators
receive notifications about new requests and can accept or reject them through a user-friendly
interface.
* User management: the ability to add, delete and modify user accounts. Administrators can
assign access rights and control user activity.
* Protocol support: WireGuard, OpenVPN (will be available soon).
* Self-hosted solution: EasyVPN is completely deployed on your own server, which provides full control over
data and settings. You can customize and scale the system depending on your needs.

## Repo Structure
The monorepo is organized as follows:

- **Backend** ([`/backend`](backend)):
  REST API server on ASP.NET.

- **Frontend** ([`/frontend`](frontend)): Web application on React.

- **Services** ([`/services`](services)):
  Services that implement VPN protocols.
  - [`/services/WireguardVPN`](services/WireguardVPN): WireGuard VPN protocol.

## Installation

1. Clone the repository:
```bash
git clone https://github.com/F1st3K/EasyVPN.git
```

2. Navigate to the project directory:
```bash
cd EasyVPN
```

3. Build and run docker containers:
```bash
docker compose up --build 
```
> `Important:` Make sure that in the frontend configuration [config.json](./frontend/src/config.json), 
> `"ApiUrl": "api"` is specified to correctly redirect requests to the api.

## Contributing

We welcome contributions from the community to improve EasyVPN. If you'd like to contribute, please fork the repository, 
make your changes, and submit a Pull Request.

[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.md
[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.ru_RU.md
