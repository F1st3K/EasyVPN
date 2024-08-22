[![readme-en-shield]][readme-en-url]
[![readme-ru-shield]][readme-ru-url]

# EasyVPN Frontend

React веб-приложение для управления EasyVPN.

## Установка

### Локальная установка вручную

Для разработки данного проекта мы используем **Node.js LTS 20**.

1. **Установите зависимости:**

```bash
npm install
```

2. **Настройте конфигурацию:**

> Конфигурация web-приложения находится в [`config.json`](./src/config.json).

Параметры `config.json`:

- `ApiUrl`: URL к развернутому [`EasyVPN backend`](../backend/README.md), с которым общается
  веб-приложение (по умолчанию: `http://localhost:80/api/`).
- `AuthCheckMinutes`: периодичность (в минутах), с которой система автоматически выполняет
  проверку текущего состояния авторизации. Также авторизация проверяется автоматически при каждом изменении
  навигации в приложении (по умолчанию: `15`).

3. **Запустите проект:**

```bash
npm run start
```

### Установка через Docker контейнер

1. **Сборка Docker образа:**

```bash
docker build -t easyvpn/frontend:local ./
```

2. **Запуск сервиса в Docker-контейнере:**

```bash
docker run -d \
  --name frontend \
  -p 3000:3000 `# port` \
  easyvpn/frontend:local
```

Веб-приложение в обоих случаях будет доступно на `http://localhost:3000`.

[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.md
[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.ru_RU.md