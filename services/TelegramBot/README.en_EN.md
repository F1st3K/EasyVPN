[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

# TelegramBot Service

A Telegram bot service designed to simplify user notifications about their connections.  
Built with **Python** and **aiogram**.

---

## Configuration

> All configuration parameters should be set in the `.env` file. Example: [.env.example](.env.example)

| Variable       | Description                                     |
|----------------|-------------------------------------------------|
| `BOT_TOKEN`    | Telegram bot token                              |
| `DEBUG`        | Enable debug mode (`True` / `False`)            |
| `DB_PATH`      | SQLite database file name                       |
| `DB_DIR`       | Path to the directory containing the database   |

---

## Local Run

Make sure you have [Poetry](https://python-poetry.org/) installed.

Install dependencies:

```bash
poetry install
```

Compile locales:

```bash
poetry run pybabel compile -d bot/locales -D messages
```

Run the bot:

```bash
poetry run python -m bot
```

---

## Run with Docker
Run the container:
```bash
docker run -d \
  --name bot-service \
  -e BOT_TOKEN=YOUR_BOT_TOKEN \
  easyvpn/bot
```

---

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md
