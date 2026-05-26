FROM golang:1.26.2 AS builder

WORKDIR /app

COPY go.mod go.sum ./
RUN go mod download

COPY . .

RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 \
    go build -ldflags="-X 'config.Version=$(cat VERSION)'" -o vlessproxy ./cmd

FROM ghcr.io/sagernet/sing-box:latest

ENTRYPOINT []

WORKDIR /app

COPY --from=builder /app/vlessproxy .

CMD ["./vlessproxy"]