package telegram

import (
	"Notification/internal/models"
	"context"
	"errors"
	"fmt"
	"log"

	tgbotapi "github.com/go-telegram-bot-api/telegram-bot-api/v5"
)

type Config struct {
	Token  string `env:"TG_TOKEN" env-required:"true"`
	ChatID int64  `env:"TG_CHAT_ID" env-required:"true"`
}

type TGSender struct {
	API    *tgbotapi.BotAPI
	ChatID int64
}

func NewTGSender(cfg Config) *TGSender {
	api, err := tgbotapi.NewBotAPI(cfg.Token)
	if err != nil {
		log.Fatalf("TG sender error: %w", err.Error())
	}
	return &TGSender{API: api, ChatID: cfg.ChatID}
}

func (t *TGSender) Send(ctx context.Context, message models.Event) error {
	text := fmt.Sprintf(
		"%s\n\n%s",
		message.Subject,
		message.Message,
	)

	msg := tgbotapi.NewMessage(t.ChatID, text)

	_, err := t.API.Send(msg)
	if err != nil {
		return errors.New("tg sender connection reset by peer")
	}
	return err
}
