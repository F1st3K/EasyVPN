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

	log.Printf("WireguardVpn api http server started on port: %s", config.Port)
	if err := srv.Run(config.Port, handler); err != nil {
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
	Port     string `yaml:"port"`
	Username string `yaml:"user"`
	Password string `yaml:"password"`
}

func NewConfig() *Config {
	return &Config{
		Port:     "0",
		Username: "",
		Password: "",
	}
}
