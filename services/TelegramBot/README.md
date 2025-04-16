[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

# TelegramBot Service

Сервис Telegram-бота, предназначенный для упрощения уведомления пользователей о подключениях.  
Реализован с использованием **Python** и **aiogram**.

---

## Конфигурация

> Все параметры конфигурации указываются в `.env` файле. Пример: [.env.example](.env.example)

| Переменная      | Описание                                      |
|-----------------|-----------------------------------------------|
| `BOT_TOKEN`     | Токен Telegram-бота                           |
| `DEBUG`         | Включение режима отладки (`True` / `False`)   |
| `DB_PATH`       | Имя файла базы данных SQLite                  |
| `DB_DIR`        | Путь к директории, где находится база данных  |

---

## Локальный запуск

Убедитесь, что у вас установлен [Poetry](https://python-poetry.org/).

Установите зависимости:

```bash
poetry install
```

Скомпилируйте локали:

```bash
poetry run pybabel compile -d bot/locales -D messages
```

Запустите бота:

```bash
poetry run python -m bot
```

---

## Запуск через Docker
Запустите контейнер:
```bash
docker run -d \
  --name bot-service \
  -e BOT_TOKEN=YOUR_BOT_TOKEN \
  easyvpn/bot
```

---

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md
