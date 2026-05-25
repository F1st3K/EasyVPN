package config

import (
	"errors"
	"log"
	"os"
)

type Config struct {
	SingBoxCfgPath string
	ServerDomain   string
	ServerName     string
	Port           string
	DBPath         string
	ShortID        string
	AdminUser      string
	AdminPassword  string
	KeyPath        string
	SingBoxPort    string
}

func Load() (*Config, error) {
	cfg := &Config{
		SingBoxCfgPath: os.Getenv("SINGBOX_CFG_PATH"),
		ServerDomain:   os.Getenv("SERVER_HOST"),
		ServerName:     os.Getenv("SERVER_NAME"),
		Port:           os.Getenv("API_PORT"),
		DBPath:         os.Getenv("DB_PATH"),
		ShortID:        os.Getenv("SHORT_ID"),
		AdminUser:      os.Getenv("SERVICE_USER"),
		AdminPassword:  os.Getenv("SERVICE_PASSWORD"),
		KeyPath:        os.Getenv("KEY_PATH"),
		SingBoxPort:    os.Getenv("SING_BOX_PORT"),
	}

	if cfg.SingBoxCfgPath == "" {
		return nil, errors.New("SINGBOX_CFG_PATH is required")
	}

	if cfg.ServerDomain == "" {
		return nil, errors.New("SERVER_HOST is required")
	}

	if cfg.ServerName == "" {
		return nil, errors.New("SERVER_NAME is required")
	}

	if cfg.Port == "" {
		cfg.Port = "8080"
	}
	log.Println("Запущен на :", cfg.Port)

	if cfg.DBPath == "" {
		cfg.DBPath = "./db.sqlite"
	}

	if cfg.ShortID == "" {
		return nil, errors.New("SHORT_ID is required")
	}

	if cfg.AdminUser == "" {
		return nil, errors.New("SERVICE_USER is required")
	}

	if cfg.AdminPassword == "" {
		return nil, errors.New("SERVICE_PASSWORD is required")
	}

	if cfg.KeyPath == "" {
		return nil, errors.New("KEY_PATH is required")
	}
	if cfg.SingBoxPort == "" {
		cfg.SingBoxPort = "444"
	}
	log.Println("Sing-box запущен на :", cfg.SingBoxPort)
	return cfg, nil
}
