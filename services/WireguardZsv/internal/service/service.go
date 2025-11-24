package service

import (
	"WireguardZsv/config"
	wireguardzsv "WireguardZsv/pkg"
	"WireguardZsv/pkg/repositories"
	"WireguardZsv/pkg/utils"
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

	wg := *utils.NewWgManager(cfg.Service.Host, cfg.Zsv.Port,
		clientRepository.GetAllClients())

	handler := wireguardzsv.NewHandler(clientRepository, wg, address)

	h := handler.InitRoutes(cfg.Service.User, cfg.Service.Password)
	srv := new(wireguardzsv.Server)

	log.Printf("WireguardZsv api http server started on port: %s", cfg.Api.Port)
	if err := srv.Run(cfg.Api.Port, h); err != nil {
		log.Fatalf("Error while ocurated http server: %s", err.Error())
	}
}
