package config

import (
	"errors"
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
}

func Load() (*Config, error) {
	cfg := &Config{
		SingBoxCfgPath: os.Getenv("SINGBOX_CFG_PATH"),
		ServerDomain:   os.Getenv("SERVER_DOMAIN"),
		ServerName:     os.Getenv("SERVER_NAME"),
		Port:           os.Getenv("PORT"),
		DBPath:         os.Getenv("DB_PATH"),
		ShortID:        os.Getenv("SHORT_ID"),
		AdminUser:      os.Getenv("ADMIN_USER"),
		AdminPassword:  os.Getenv("ADMIN_PASSWORD"),
		KeyPath:        os.Getenv("KEY_PATH"),
	}

	if cfg.SingBoxCfgPath == "" {
		return nil, errors.New("SINGBOX_CFG_PATH is required")
	}

	if cfg.ServerDomain == "" {
		return nil, errors.New("SERVER_DOMAIN is required")
	}

	if cfg.ServerName == "" {
		return nil, errors.New("SERVER_NAME is required")
	}

	if cfg.Port == "" {
		cfg.Port = "8080"
	}

	if cfg.DBPath == "" {
		cfg.DBPath = "./db.sqlite"
	}

	if cfg.ShortID == "" {
		return nil, errors.New("SHORT_ID is required")
	}

	if cfg.AdminUser == "" {
		return nil, errors.New("ADMIN_USER is required")
	}

	if cfg.AdminPassword == "" {
		return nil, errors.New("ADMIN_PASSWORD is required")
	}

	if cfg.KeyPath == "" {
		return nil, errors.New("KEY_PATH is required")
	}

	return cfg, nil
}
