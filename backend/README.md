# EasyVPN backend
Приложение бизнес-логики, имеющее понятное rest-api. (C# + ASP.NET Core)


## Configuration
> Конфигурация web-интерфейса находится в [`appsettings.json`](./src/EasyVPN.Api/appsettings.json):

`"Logging": { ... },` - настройки логгирования

`"AllowedHosts": "*",` - разрешенные хосты для обращения

`"JwtSettings":` - настройки jwt токенов авторизации:

 - `"Secret": "super-puper-mega-secret-key-solo",` - секрет защищающий токены
 - `"ExpiryMinutes": 60,` - срок жизни каждого отдельного токена после его создания

 - `"Issuer": "EasyVPN",` и `"Audience": "EasyVPN"` - подпись кем и для кого выпускаются токены

`"HashSettings": { "Secret": "super-puper-mega-hash-soltt-solo" },` - секрет использующийся при хэшировании паролей

`"ExpireSettings": { "CheckMinutes": 1 },` - переодичность в минутах, для проверки истекших обьектов (токен авторизации, vpn-подключений и т.п.)

`"ConnectionStrings": { "Postgres": "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;" }` - Строки подключения к базам данных
(главная и необходимая к PostgreSQL, при не удачной попытке подключения приложение не поднимется)

> Так же для более безопастного конфигурирования можно использовать enviroment переменные, заменяющие основную конфигурацию, например:

`ConnectionStrings__Postgres` - заменит строку подключения к базе данных


## Deploy (docker)
Сборка докер-образа:
```bash
docker build -t easyvpn/backend:local ./
```

Для запуска базы данных необходимой для работы приложения бизнес-логики в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name database \
  -e POSTGRES_USER=postgres `#your db user`\
  -e POSTGRES_PASSWORD=mysecretpassword `#your db password`\
  -p 5432:5432 `#your port`\
  -v easyvpn_database_data:/var/lib/postgresql/data \
  postgres:latest
```

Для запуска приложения бизнес-логики в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name backend \
  -p 80:80 `#your port`\
  easyvpn/backend:local
```

## EasyVPN API
Для изучения API вам нужно запустить приложение бизнес-логики и перейти в интерфейс [`/swagger/`](http://localhost/swagger/).
