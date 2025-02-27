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
	os.MkdirAll(ClientsPath, os.ModeAppend)
	os.MkdirAll(AddressPath, os.ModeAppend)

	clientRepository := *repositories.NewClientRepoitory(ClientsPath)
	address := *utils.NewAddressManager(AddressPath)

	wg := *utils.NewWgManager(cfg.Service.Host, cfg.Vpn.Port,
		clientRepository.GetAllClients())

	handler := wireguardvpn.NewHandler(clientRepository, wg, address)

	h := handler.InitRoutes(cfg.Service.User, cfg.Service.Password)
	srv := new(wireguardvpn.Server)

	log.Printf("WireguardVpn api http server started on port: %s", cfg.Api.Port)
	if err := srv.Run(cfg.Api.Port, h); err != nil {
		log.Fatalf("Error while ocurated http server: %s", err.Error())
	}
}
