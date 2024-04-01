package main

import (
	wireguardvpn "WireguardVpn/pkg"
	"WireguardVpn/pkg/repositories"
	"WireguardVpn/pkg/utils"
	"log"
	"os"

	"gopkg.in/yaml.v3"
)

const (
	CLILENTS_PATH = utils.WG_DIR + "/clients"
	ADDRESS_PATH  = utils.WG_DIR + "/address"
)

func main() {
	config := loadConfig()
	host := os.Getenv("HOST")

	os.MkdirAll(CLILENTS_PATH, os.ModeAppend)
	clientRepository := *repositories.NewClientRepoitory(CLILENTS_PATH)

	os.MkdirAll(ADDRESS_PATH, os.ModeAppend)
	address := *utils.NewAddressManager(ADDRESS_PATH)

	wg := *utils.NewWgManager(host, config.WgPort,
		clientRepository.GetAllClients())

	handler := wireguardvpn.NewHandler(clientRepository, wg, address)

	h := handler.InitRoutes(config.Username, config.Password)
	srv := new(wireguardvpn.Server)

	log.Printf("WireguardVpn api http server started on port: %s", config.ApiPort)
	if err := srv.Run(config.ApiPort, h); err != nil {
		log.Fatalf("Error while ocurated http server: %s", err.Error())
	}

}

func loadConfig() *Config {
	config := NewConfig()

	file, err := os.ReadFile("./config.yaml")
	if err != nil {
		log.Fatalf("Error #%v", err)
	}

	err = yaml.Unmarshal(file, config)
	if err != nil {
		log.Fatalf("Error #%v", err)
	}

	return config
}

type Config struct {
	ApiPort  string `yaml:"api_port"`
	WgPort   string `yaml:"wg_port"`
	Username string `yaml:"user"`
	Password string `yaml:"password"`
}

func NewConfig() *Config {
	return &Config{
		ApiPort:  "0",
		WgPort:   "0",
		Username: "",
		Password: "",
	}
}
