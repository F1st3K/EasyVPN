[![readme-en-shield]][readme-en-url]
[![readme-ru-shield]][readme-ru-url]

# EasyVPN Frontend

React web app for managing EasyVPN.

## Installation

### Manual local installation

For the development of this project we use **Node.js LTS 20**.

1. **Install dependencies:**

```bash
npm install
```

2. **Setup configuration:**

> The web application configuration is located in [`config.json`](./src/config.json).

- `ApiUrl`: URL to deployed [`EasyVPN backend`](../backend/README.md) that the web application communicates 
with (default: `http://localhost:80/api/`).
- `AuthCheckMinutes`: frequency (in minutes) with which the system automatically checks the current authorization 
status. Authorization is also checked automatically every time the navigation in the application 
changes (default: `15`).

3. **Launch the project:**

```bash
npm run start
```

### Installation via docker container

1. Build a docker image:

```bash
docker build -t easyvpn/frontend:local ./
```

2. Run a service in a docker container:

```bash
docker run -d \
  --name frontend \
  -p 3000:3000 `# port` \
  easyvpn/frontend:local
```

The web application will be available in both cases on [``http://localhost:3000``](http://localhost:3000).

[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.md
[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.ru_RU.md