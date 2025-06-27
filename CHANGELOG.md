# Changelog

## `1.1.1` (2025-06-27)

### Fixes

- frontend: Fix login on press enter
- backend: Fix error 405 on update dynamic pages

## `1.1.0` (2025-05-22)

### Features

- backend: Hide swagger documentation by env variable
- backend: Add version for response health
- Single Deploy: Reject HTTPS in app
- Single Deploy: Add deployment for dev and prod enviroment by workflow 

## `1.0.0` (2025-05-20)

### Features

- frontend: Create user profile, editable any auth users 
- frontend: Create connections panel, editable ConnectionsRegulator role 
- frontend: Create servers and protocols panel, editable ServerSetuper role 
- frontend: Create users panel, editable SecurityKeeper role 
- backend: Create user profile controller
- backend: Create administrate connection controller
- backend: Create administrate server and protocol controllers
- backend: Create administrate user controller
- Single Deploy: Switch proxy to HTTPS

### Infrastructure

- Update single deployment - add certbot service (for auto newelble certs)

### Documentation

- Performing main README

## `0.7.2` (2025-04-18)

### Hotfixes

- init: Proceed invalid character in db password
- frontend: fix env vars, replace to nginx template
- Single Deploy: add restart always policy

## `0.7.1` (2025-04-16)

### Hotfixes

- init: Add dynamic-pages
- frontend: Fixed incorrect display of dynamic-page content 
- frontend: Fixed errors on build workflow 
- backend: Fixed errors on build workflow 
- Fixed errors on Single Deploy workflow 

### Documentation

- Performing repository
- All missing documentation added
- Add EN to all documention
- VPN API versions has been created and moved to a separate document. 

## `0.7.0` (2025-04-11)

### Features

- frontend: Create Dynamic-Pages, editable PageModerator role 
- backend: Create endpoint for dynamic-pages
- services: Create new service - TelegramBot (for notifiactaioin etc.)
- services: TelegramBot - add command /start
- services: TelegramBot - add locales 
- services: Add new service - init (for inittialise base data for app)

### Infrastructure

- Update single deployment - add all services and replace .env varibles
- Add Workflows for services

## `0.6.0` (2025-02-27)

### Features

- services: Create new Amnezia WG vpn-service (wireguard based)
- services: Update documentation

### Bug fixes

- services: Fix reload wireguard to resync configuration (fasted)
- services: Fix env variables with ports (WG_PORT -> VPN_PORT)

## `0.5.1` (2024-12-08)

### Hotfixes

- backend: Fixed exception on create Sheduled task (error task - key not found)

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
