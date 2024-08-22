[![readme-en-shield]][readme-en-url]
[![readme-ru-shield]][readme-ru-url]

# EasyVPN

EasyVPN это self-hosted сервис для управления и контроля VPN-подключений, который 
упрощает процесс администрирования VPN-сетей. Проект включает в себя серверную и 
клиентскую часть, обеспечивая возможность централизованного управления доступом к VPN 
на вашем собственном сервере.

## Возможности

* Управление VPN-подключениями: EasyVPN позволяет администраторам контролировать активные VPN-сессии,
  управлять пользователями и их доступом к сетевым ресурсам.
* Запросы на подключение: пользователи могут отправлять запросы на VPN-подключение через систему. Администраторы
получают уведомления о новых запросах и могут принимать или отклонять их через удобный интерфейс.
* Управление пользователями: возможность добавлять, удалять и изменять учетные записи пользователей. Администраторы могут
  назначать права доступа и контролировать активность пользователей.
* Поддержка протоколов: WireGuard, OpenVPN (будет доступен в ближайшее время).
* Решение с собственным размещением: EasyVPN полностью развертывается на вашем собственном сервере, что 
обеспечивает полный контроль над данными и настройками. Вы можете настраивать и масштабировать 
систему в зависимости от своих потребностей.

## Структура репозитория
Монорепозиторий организован следующим образом:

- **Бэкенд** ([`/backend`](backend)):
  REST API сервер на ASP.NET.

- **Фронтенд** ([`/frontend`](frontend)): Веб-приложение на React.

- **Сервисы** ([`/services`](services)):
  Сервисы, реализующие VPN протоколы.
  - [`/services/WireguardVPN`](services/WireguardVPN): Протокол WireGuard VPN.

## Установка

1. Клонируйте репозиторий:
```
git clone https://github.com/F1st3K/EasyVPN.git
```

2. Перейдите в каталог проекта:
```
cd EasyVPN
```

3. Соберите и запустите docker контейнеры:
```
docker compose up --build
```
> `Важно:` Убедитесь, что в конфигурации фронтенда [config.json](./frontend/src/config.json) указано
> `"ApiUrl": "api"` для корректного перенаправления запросов в api.

## Контрибьюция

Мы приветствуем контрибьюцию в улучшение EasyVPN. Если вы хотите помочь, пожалуйста, создайте fork репозитория,
внесите изменения и отправьте Pull Request.

[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.md
[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.ru_RU.md