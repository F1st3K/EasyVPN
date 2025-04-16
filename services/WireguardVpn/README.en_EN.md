[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

# Service WireguardVpn
A service application for working with WireGuard connections that has a simple http-api. (Golang + gin)

## Configuration
> The service configuration is in [config.yaml](./cmd/config.yaml):

`api: port: "8000"` - the port on which the http-api will work (or via the env variable API_PORT)

`vpn: port: "51820"` - the port through which the WireGuard connection is established (or via the env variable VPN_PORT)

> Also, for the service to work, the enviroment variables listed in [.env.example](./cmd/.env.example) are additionally required:

`SERVICE_HOST` - the host address on which the service will be launched

`SERVICE_USER` and `SERVICE_PASSWORD` - username and password for basic auth to http-api

## Deploy (docker)
To run the service in a docker container, use the following command:
```bash
docker run -d\
 --name wireguard-vpn-service\
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