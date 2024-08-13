# EasyVPN backend
Приложение бизнес-логики, имеющее понятное rest-api. (C#, ASP.NET Core)


## Configuration
> Конфигурация web-интерфейса находится в [appsettings.json](./src/EasyVPN.Api/appsettings.json):

...

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
  -v ~/postgresql/:/var/lib/postgresql/data \
  postgres:latest
```

Для запуска приложения бизнес-логики в докер-контейнере используйте следующую команду:
```bash
docker run -d \
  --name backend \
  -p 80:80 `#your port`\
  easyvpn/backend:local
```

