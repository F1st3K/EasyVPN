package config

import (
	"Notification/internal/rabbit"
	"Notification/internal/senders/mail"
	"Notification/internal/senders/telegram"
	"log"

	"github.com/ilyakaznacheev/cleanenv"
	"github.com/joho/godotenv"
)

var Version = "dev"

type Config struct {
	Rabbit   rabbit.Config
	Mail     mail.Config
	Telegram telegram.Config
	Version  string
}

func Load() (*Config, error) {
	cfg := &Config{}

	_ = godotenv.Load()

	if err := cleanenv.ReadEnv(cfg); err != nil {
		log.Fatalln("Load config error:", err.Error())
	}

	cfg.Version = Version

	log.Println("Notification service version:", cfg.Version)

	return cfg, nil
}
