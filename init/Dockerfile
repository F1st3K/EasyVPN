FROM alpine:latest

RUN apk add --no-cache curl postgresql-client base64 file

WORKDIR /usr/src/init
COPY . . 
RUN chmod +x ./init.sh

ENTRYPOINT ["./init.sh"]
