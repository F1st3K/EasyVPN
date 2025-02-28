FROM python:3.12-alpine

WORKDIR /app

RUN apk add --no-cache curl build-base && \
    curl -sSL https://install.python-poetry.org | python3 - && \
    apk del curl build-base

ENV PATH="/root/.local/bin:${PATH}"

COPY pyproject.toml poetry.lock ./

RUN poetry install --no-root --no-interaction --no-ansi

COPY . .

RUN poetry install --no-interaction --no-ansi

CMD ["poetry", "run", "python3", "main.py"]