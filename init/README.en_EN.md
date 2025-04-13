[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

# EasyVPN init
Data initialization service, very simple and fast, which climbs into api and db (bash + SQL)

## Configuration
> To start the service, the enviroment variables listed in [.env.example](./cmd/.env.example) are required:

`API` - URL before the running backend.

`DB_CONNECTION_STRING` - connection string to the main DB.

`CREATE_SECURITY_KEEPER` - optional - variable with login:password creating a user with rights, the user is not created if the variable is specified.

## Run (docker):
To run initialization in a docker container, use the following command:

```sh
docker run -d \
--name init \
-e API=http//localhost:80/api \
-e DB_CONNECTION_STRING=postgresql://postgres:mysecretpassword@localhost:5432 \
-e CREATE_SECURITY_KEEPER=admin:admin \
easyvpn/init
```