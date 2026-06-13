FROM golang:1.26.3-alpine AS builder

RUN apk add --no-cache ca-certificates
WORKDIR /usr/local/src

COPY ["./go.mod", "./go.sum", "./"]
RUN go mod download

COPY ./ ./
RUN go build -o ./bin/NotificationService ./app/cmd/main.go

FROM alpine AS runner

COPY --from=builder /usr/local/src/bin/NotificationService ./
COPY ./app/cmd/.env ./
CMD ["./NotificationService"]