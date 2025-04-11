FROM python:3.12-alpine

ENV POETRY_NO_INTERACTION=1 \
    POETRY_VIRTUALENVS_IN_PROJECT=1 \
    POETRY_VIRTUALENVS_CREATE=0 \
    POETRY_HOME="/etc/poetry" \
    POETRY_CACHE_DIR="/tmp/poetry_cache" \
    POETRY_VERSION=2.0.1

WORKDIR /usr/src/bot

COPY . .

RUN pip install --no-cache-dir "poetry==$POETRY_VERSION" \
    && poetry install --without dev --no-root \
    && pip uninstall -y poetry \
    && pybabel compile -d bot/locales \
    && rm -rf /home/bot/.cache \
    && rm -rf $POETRY_CACHE_DIR

CMD ["python", "-m", "bot"]
