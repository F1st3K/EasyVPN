# Changelog

## `0.5.0` (2024-12-07)

### Features

- backend: Replaced ExpirationChecker by universal SheduledTaskService
> (now you can set a delayed launch of commands)

### Bug fixes

- frontend: Fixed most npm warnings
- backend: Fixed not disconnecting connections when their lifespan expires

## `0.4.1` (2024-10-14)

### Hotfixes

- Postgress version in docker-compose set as 16
- frontend: Replace 'npm start' on nginx server in docker-image
- frontend: Added cashe in nginx configuration in docker-image
- frontend: fix logo image source

## `0.4.0` (2024-10-12)

### Features

- frontend: Created payment tickets list for payment reviewer
- frontend: Added show more info of ticket
- frontend: Added confirm and reject tcket for payment reviewer
- frontent: Created Logotype for project (any icons with it)
- backend: Create missing routes for it (connecitons, tickets and servers)

### Bug fixes

- frontend: Fix other ui-components of added missing routes
- backend: Fix forbbiden errors to currectly error messages

## `0.3.0` (2024-09-18)

### Features

- frontend: Added Delete and Create connection buttons for client
- frontend: Added Extend and Create connection forms (and more components for it)

### Bug fixes

- backend: Fix CORS errors

### Infrastructure

- backend: Update dotnet sdk to version 8 and update dependences

## `0.2.0` (2024-08-22)

### Features

- backend: Improved work with different (and adding new) versions of VPN services
- services/WireguardVpn: Version moved to new route (`/ -> /v1/`)

### Documentation

- backend: Fix deployment postgress in docker

## `0.1.3` (2024-08-15)

### Features

- Create swagger endpoint in EasyVPN.Api

### Documentation

- Create & refactoring README.md files (backend, frontend, services/WireguardVpn)
- Transferred EasyVPN.Api documention to swagger
- Simplifictaion main README.md

## `0.1.2`

### Bug fixes

- publish versioning script

## `0.1.1`
## `0.1.0` (2024-08-02)

### First release

- configured versioning
