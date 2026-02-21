package main

import (
	"log"

	"wireguardvpn/internal/service"
	"wireguardvpn/pkg/config"
)

func main() {
	cfg, err := config.NewConfig()
	if err != nil {
		log.Fatalf("Config error: %s", err)
	}

	service.Start(cfg)
}
