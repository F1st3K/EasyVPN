FROM golang:1.22-alpine AS builder

RUN apk --no-cache add bash git make gettext
WORKDIR /usr/local/src

# Dependencies:
COPY ["./go.mod", "./go.sum", "./"]
RUN go mod download

# Build:
COPY ./ ./
RUN go build -o ./bin/WireguardZsv cmd/main.go

FROM alpine AS runner

RUN apk --no-cache add wireguard-tools iptables
WORKDIR /usr/local/src

# Run WireguardZsv service:
COPY --from=builder /usr/local/src/bin/WireguardZsv ./
COPY cmd/config.yml cmd/.env.example ./
CMD ["./WireguardZsv"]