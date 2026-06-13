package di

import (
	"Notification/internal/config"
	"Notification/internal/rabbit"
	mail "Notification/internal/senders/mail"
	"Notification/internal/senders/telegram"
	"context"
	"log"
	"os/signal"
	"sync"
	"syscall"
)

type DI struct {
	Senders  []rabbit.Sender
	Consumer *rabbit.Consumer
	Config   *config.Config
}

func (d *DI) GetConfig() *config.Config {
	if d.Config != nil {
		return d.Config
	}
	cfg, err := config.Load()
	if err != nil {
		log.Fatalln(err.Error())
	}
	d.Config = cfg
	return d.Config
}

func (d *DI) GetSenders() []rabbit.Sender {
	if d.Senders != nil {
		return d.Senders
	}
	cfg := d.GetConfig()

	var senders []rabbit.Sender
	smtp := mail.NewSMTPSender(cfg.Mail)
	senders = append(senders, smtp)

	telegram := telegram.NewTGSender(cfg.Telegram)
	senders = append(senders, telegram)

	d.Senders = senders
	return d.Senders
}

func (d *DI) GetConsumer() *rabbit.Consumer {
	if d.Consumer != nil {
		return d.Consumer
	}
	cfg := d.GetConfig()
	consumer := rabbit.NewConsumer(cfg.Rabbit, d.GetSenders())

	d.Consumer = consumer
	return d.Consumer
}

func (d *DI) ExecProcess(ctx context.Context) {
	consumer := d.GetConsumer()
	consumer.Process(ctx)
}

func Run() {
	di := &DI{}
	ctx, cancel := signal.NotifyContext(context.Background(), syscall.SIGTERM, syscall.SIGINT)
	defer cancel()
	wg := sync.WaitGroup{}
	wg.Add(1)
	go func() {
		defer wg.Done()
		di.ExecProcess(ctx)
		di.Consumer.Close()
	}()
	wg.Wait()
}
