[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md

# NotificationEasyVPN

Сервис уведомлений на базе RabbitMQ.

Приложение получает события из очереди RabbitMQ и отправляет уведомления через:

* Email (SMTP)
* Telegram Bot API

## Формат события

Сообщение в очереди должно содержать JSON следующего формата:

```json
{
  "subject": "Тема сообщения",
  "message": "Текст сообщения"
}
```

## Переменные окружения

Пример `.env`:

```env
RABBIT_URI=amqp://guest:guest@rabbitmq:5672/
RABBIT_QUEUE=test-queue

SMTP_HOST=smtp.gmail.com
SMTP_LOGIN=example@gmail.com
SMTP_PASSWORD=your_app_password
SMTP_FROM=example@gmail.com
SMTP_TO_EMAIL=target@gmail.com
SMTP_PORT=587

TG_TOKEN=your_bot_token
TG_CHAT_ID=123456789
```

## Сборка Docker-образа

```bash
docker build -t notificationeasyvpn .
```

## Запуск RabbitMQ

```bash
docker network create notification-net

docker run -d \
  --name rabbitmq \
  --network notification-net \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:3-management
```
## Запуск NotificationEasyVPN

```bash
docker run -d \
  --name NotificationEasyVPN \
  --network notification-net \
  notificationeasyvpn
```