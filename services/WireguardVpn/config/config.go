package config

import (
	"github.com/ilyakaznacheev/cleanenv"
	"github.com/joho/godotenv"
)

type (
	Config struct {
		Service `yaml:"service"`
		Api     `yaml:"api"`
		Wg      `yaml:"wg"`
	}

	Service struct {
		Name     string `env-required:"true" yaml:"name"    env:"APP_NAME"`
		Version  string `env-required:"true" yaml:"version" env:"APP_VERSION"`
		Host     string `env-required:"true" env:"SERVICE_HOST"`
		User     string `env-required:"true" env:"SERVICE_USER"`
		Password string `env-required:"true" env:"SERVICE_PASSWORD"`
	}

	Api struct {
		Port string `env-required:"true" yaml:"port" env:"API_PORT"`
	}

	Wg struct {
		Port string `env-required:"true" yaml:"port" env:"WG_PORT"`
	}
)

func NewConfig() (*Config, error) {
	cfg := &Config{}

	err := godotenv.Load("./.env.example")
	if err != nil {
		return nil, err
	}

	err = cleanenv.ReadConfig("./config.yml", cfg)
	if err != nil {
		return nil, err
	}

	err = cleanenv.ReadEnv(cfg)
	if err != nil {
		return nil, err
	}

	return cfg, nil
}
