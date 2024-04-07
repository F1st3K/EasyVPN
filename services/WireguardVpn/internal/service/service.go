package service

import (
	"WireguardVpn/config"
	wireguardvpn "WireguardVpn/pkg"
	"WireguardVpn/pkg/repositories"
	"WireguardVpn/pkg/utils"
	"log"
	"os"
)

const (
	ClientsPath = utils.WgDir + "/clients"
	AddressPath = utils.WgDir + "/address"
)

func Start(cfg *config.Config) {
	err := os.MkdirAll(ClientsPath, os.ModeAppend)
	if err != nil {
		return
	}
	clientRepository := *repositories.NewClientRepoitory(ClientsPath)

	os.MkdirAll(AddressPath, os.ModeAppend)
	address := *utils.NewAddressManager(AddressPath)

	wg := *utils.NewWgManager(cfg.Service.Host, cfg.Wg.Port,
		clientRepository.GetAllClients())

	handler := wireguardvpn.NewHandler(clientRepository, wg, address)

	h := handler.InitRoutes(cfg.Service.Username, cfg.Service.Password)
	srv := new(wireguardvpn.Server)

	log.Printf("WireguardVpn api http server started on port: %d", cfg.Api.Port)
	if err := srv.Run(cfg.Api.Port, h); err != nil {
		log.Fatalf("Error while ocurated http server: %s", err.Error())
	}
}
