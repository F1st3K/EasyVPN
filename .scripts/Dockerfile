FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get update && apt-get install -y nodejs
RUN dotnet tool install --global dotnet-setversion
RUN wget https://github.com/mikefarah/yq/releases/download/v4.20.2/yq_linux_amd64  -O /usr/bin/yq &&\
    chmod +x /usr/bin/yq
RUN curl -sSL https://install.python-poetry.org | python3 -

ENV PATH="/root/.local/bin:$PATH"
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /app

