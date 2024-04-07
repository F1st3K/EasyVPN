package main

import (
	"WireguardVpn/config"
	"WireguardVpn/internal/service"
	"log"
)

func main() {
	cfg, err := config.NewConfig()
	if err != nil {
		log.Fatalf("Config error: %s", err)
	}

	service.Start(cfg)
}
