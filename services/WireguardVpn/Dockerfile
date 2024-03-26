FROM golang:1.22-alpine AS builder

WORKDIR /usr/local/src

RUN apk --no-cache add bash git make gettext

COPY ["./go.mod", "./go.sum", "./"]
RUN go mod download

COPY ./ ./
RUN go build -o ./bin/app cmd/main.go

FROM alpine AS runner

RUN apk --no-cache add wireguard-tools

COPY --from=builder /usr/local/src/bin/app /
COPY cmd/config.yaml /
CMD ["/app"]