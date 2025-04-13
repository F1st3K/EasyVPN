[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

# EasyVPN backend
A business logic application with a clear rest-api. (C# + ASP.NET Core)

## Configuration
> Web interface configuration is in [`appsettings.json`](./src/EasyVPN.Api/appsettings.json):

`"Logging": { ... },` - logging settings

`"AllowedHosts": "*",` - allowed hosts for access

`"JwtSettings":` - jwt authorization token settings:

- `"Secret": "super-puper-mega-secret-key-solo",` - secret protecting tokens
- `"ExpiryMinutes": 60,` - lifetime of each individual token after its creation

- `"Issuer": "EasyVPN",` and `"Audience": "EasyVPN"` - signature by whom and for whom tokens are issued

`"HashSettings": { "Secret": "super-puper-mega-hash-soltt-solo" },` - secret used when hashing passwords

`"ExpireSettings": { "CheckMinutes": 1 },` - frequency in minutes, for checking expired objects (authorization token, vpn connections, etc.)

`"ConnectionStrings": { "Postgres": "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;" }` - Database connection strings
(the main and necessary for PostgreSQL, if the connection attempt fails, the application will not start)

> Also, for a more secure configuration, you can use enviroment variables that replace the main configuration, for example:

`ConnectionStrings__Postgres` - will replace the database connection string

## Deploy (docker)
To launch the database required for the business logic application in a docker container, use the following command:
```bash
docker run -d \
--name database \
-e POSTGRES_USER=postgres `#your db user`\
-e POSTGRES_PASSWORD=mysecretpassword `#your db password`\
-p 5432:5432 `#your port`\
-v easyvpn_database_data:/var/lib/postgresql/data \
postgres:latest
```

To run the business logic application in a docker container, use the following command:
```bash
docker run -d \
--name backend \
-e ConnectionStrings__Postgres='User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;' \
-p 80:80 `#your port`\
easyvpn/backend:latest
```

## EasyVPN API
To explore the API, you need to run the business logic application and navigate to the [`/swagger/`](http://localhost/swagger/) interface.