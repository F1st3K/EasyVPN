package main

import (
	wireguardvpn "WireguardVpn/pkg"
	"log"
	"os"

	"gopkg.in/yaml.v3"
)

func main() {
	config := loadConfig()

	srv := new(wireguardvpn.Server)
	handler := new(wireguardvpn.Handler).InitRoutes(config.Username, config.Password)

	wireguardvpn.StartUp(config.WgPort)

	log.Printf("WireguardVpn api http server started on port: %s", config.ApiPort)
	if err := srv.Run(config.ApiPort, handler); err != nil {
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
