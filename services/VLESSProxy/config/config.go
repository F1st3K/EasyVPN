package config

import (
	"log"

	"github.com/ilyakaznacheev/cleanenv"
	"github.com/joho/godotenv"
)

var Version = "dev"

type Config struct {
	DBPath         string `env:"DB_PATH" env-default:"/etc/vlessproxy/db.sqlite"`
	KeyPath        string `env:"KEY_PATH" env-default:"/etc/vlessproxy/keys.json"`
	SingBoxCfgPath string `env:"SINGBOX_CFG_PATH" env-default:"/etc/vlessproxy/singbox.json"`
	Port           string `env:"API_PORT" env-default:"8080"`
	SingBoxPort    int    `env:"SINGBOX_PORT" env-default:"443"`
	ShortID        string `env:"SHORT_ID" env-default:"b2049af5145eba3f"`
	ServerName     string `env:"MASK_DOMAIN" env-default:"ya.ru"`
	ServerDomain   string `env:"SERVICE_HOST" env-required:"true"`
	AdminUser      string `env:"SERVICE_USER" env-required:"true"`
	AdminPassword  string `env:"SERVICE_PASSWORD" env-required:"true"`
	LogLevel       string `env:"LOG_LEVEL" env-default:"info"`
	Version        string
}

func Load() (*Config, error) {
	_ = godotenv.Load()

	cfg := &Config{}

	if err := cleanenv.ReadEnv(cfg); err != nil {
		return nil, err
	}
	cfg.Version = Version

	log.Printf(
		"VLESSProxy version: %s, api http server started on port: %s",
		cfg.Version,
		cfg.Port,
	)

	log.Printf(
		"Sing-box started on port: %v",
		cfg.SingBoxPort,
	)

	return cfg, nil
}
