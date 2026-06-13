[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md

# NotificationEasyVPN

Notification service based on RabbitMQ.

Consumes events from RabbitMQ queue and delivers notifications through:

* SMTP (Email)
* Telegram Bot API

## Event format

```json
{
  "subject": "Test subject",
  "message": "Test message"
}
```

## Environment variables

```env
RABBIT_URI=amqp://guest:guest@rabbitmq:5672/
RABBIT_QUEUE=test-queue

SMTP_HOST=smtp.gmail.com
SMTP_LOGIN=example@gmail.com
SMTP_PASSWORD=app_password
SMTP_FROM=example@gmail.com
SMTP_TO_EMAIL=target@gmail.com
SMTP_PORT=587

TG_TOKEN=...
TG_CHAT_ID=...
```

## Build

```bash
docker build -t notificationeasyvpn .
```

## Run

```bash
docker run -d \
  --name NotificationEasyVPN \
  --network notification-net \
  notificationeasyvpn
```

## RabbitMQ

Example RabbitMQ container:

```bash
docker run -d \
  --name rabbitmq \
  --network notification-net \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:3-management
```
