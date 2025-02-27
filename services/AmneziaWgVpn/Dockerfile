FROM golang:1.22-alpine AS builder

RUN apk --no-cache add bash git make gettext
WORKDIR /usr/local/src

# Dependencies:
COPY ["./go.mod", "./go.sum", "./"]
RUN go mod download

# Build:
COPY ./ ./
RUN go build -o ./bin/AmneziaWgVpn cmd/main.go

FROM amneziavpn/amneziawg-go AS runner

# RUN apk --no-cache add amneziawg iptables
WORKDIR /usr/local/src

# Run AmneziaWgVpn service:
COPY --from=builder /usr/local/src/bin/AmneziaWgVpn ./
COPY cmd/config.yml cmd/.env.example ./
CMD ["./AmneziaWgVpn"]
