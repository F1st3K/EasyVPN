package service

import (
	"log"
	"os"

	"wireguardvpn/pkg/config"
	"wireguardvpn/pkg/entities"
	"wireguardvpn/pkg/repositories"
	"wireguardvpn/pkg/utils"
)

const (
	ClientsPath = utils.WgDir + "/clients"
	AddressPath = utils.WgDir + "/address"
)

func Start(cfg *config.Config) {
	os.MkdirAll(ClientsPath, os.ModeAppend)
	os.MkdirAll(AddressPath, os.ModeAppend)

	clientRepository := *repositories.NewClientRepository(ClientsPath)
	addressManager := *utils.NewAddressManager(AddressPath)

	// Initialize with empty clients slice since we'll load them later
	emptyClients := make([]entities.Client, 0)
	wgManager := *utils.NewWgManager(cfg.Service.Host, cfg.Vpn.Port, emptyClients)

	vpnService := NewVPNService(cfg, &clientRepository, &wgManager, &addressManager)

	if err := vpnService.Start(); err != nil {
		log.Fatalf("Failed to start VPN service: %v", err)
	}
}
