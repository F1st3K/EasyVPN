# EasyVPN
Простое VPN + Backend + Frontend приложения на одном сервере!

## Single Deploy (docker-compose)
Для развертывания `EasyVPN` на одном единственном сервере используйте `Docker Compose` :
```bash
docker compose up --build
```
> `Важно:` При таком варианте развертывания убедитесь что в frontend-конфигурации [config.json](./frontend/src/config.json), указано `"ApiUrl": "api"` для правильного перенаправления запросов к api.

Так же для независимого развертывания компонентов web-приложения `EasyVPN`, можно воспользоваться документацией для каждого проекта:
[EasyVPN backend](backend/README.md) и [EasyVPN frontend](backend/README.md), а так же [WireguardVpn](services/WireguardVpn/README.md).

