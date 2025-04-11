## Init service

#### Example:

```sh
docker run -d \
  --name init \
  -e API=http//localhost:80/api \
  -e DB_CONNECTION_STRING=postgresql://postgres:mysecretpassword@localhost:5432 \
  -e CREATE_SECURITY_KEEPER=admin:admin \
  easyvpn/init
```