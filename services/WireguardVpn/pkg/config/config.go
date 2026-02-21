package config

import (
	"log"

	"github.com/ilyakaznacheev/cleanenv"
)

// Config holds the application configuration
type Config struct {
	Api     ApiConfig     `yaml:"api" env-prefix:"API_"`
	Service ServiceConfig `yaml:"service" env-prefix:"SERVICE_"`
	Vpn     VpnConfig     `yaml:"vpn" env-prefix:"VPN_"`
}

// ApiConfig holds API-related configuration
type ApiConfig struct {
	Port string `yaml:"port" env:"PORT" env-default:"8000"`
}

// ServiceConfig holds service-related configuration
type ServiceConfig struct {
	Host     string `yaml:"host" env:"HOST" env-default:"localhost"`
	User     string `yaml:"user" env:"USER" env-default:"admin"`
	Password string `yaml:"password" env:"PASSWORD" env-default:"password"`
}

// VpnConfig holds VPN-related configuration
type VpnConfig struct {
	Port string `yaml:"port" env:"PORT" env-default:"51820"`
}

// NewConfig creates and validates a new configuration
func NewConfig() (*Config, error) {
	cfg := &Config{}

	err := cleanenv.ReadConfig("./config.yml", cfg)
	if err != nil {
		log.Printf("Config file not found, using environment variables: %v", err)
		err = cleanenv.ReadEnv(cfg)
		if err != nil {
			return nil, err
		}
	}

	return cfg, nil
}
