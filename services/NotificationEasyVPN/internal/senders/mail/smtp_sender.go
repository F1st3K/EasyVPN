package mail

import (
	"Notification/internal/models"
	"context"
	"net/smtp"
)

type Config struct {
	Host     string `env:"SMTP_HOST" env-required:"true"`
	Login    string `env:"SMTP_LOGIN" env-required:"true"`
	Password string `env:"SMTP_PASSWORD" env-required:"true"`
	From     string `env:"SMTP_FROM_EMAIL" env-required:"true"`
	To       string `env:"SMTP_TO_EMAIL" env-required:"true"`
	Port     string `env:"SMTP_PORT" env-required:"true"`
}

type SMTPSender struct {
	Host     string
	Login    string
	Password string
	From     string
	To       string
	Port     string
}

func NewSMTPSender(cfg Config) *SMTPSender {
	return &SMTPSender{
		Host:     cfg.Host,
		Login:    cfg.Login,
		Password: cfg.Password,
		From:     cfg.From,
		To:       cfg.To,
		Port:     cfg.Port,
	}
}

func (s *SMTPSender) Send(ctx context.Context, event models.Event) error {

	msg := []byte(
		"From: " + s.From + "\r\n" +
			"To: " + s.To + "\r\n" +
			"Subject: " + event.Subject + "\r\n" +
			"\r\n" +
			event.Message,
	)
	auth := smtp.PlainAuth("", s.Login, s.Password, s.Host)

	err := smtp.SendMail(s.Host+":"+s.Port, auth, s.From, []string{s.To}, msg)
	if err != nil {
		return err
	}
	return nil
}
