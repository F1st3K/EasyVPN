package rabbit

import (
	"Notification/internal/models"
	"context"
	"encoding/json"
	"log"

	amqp "github.com/rabbitmq/amqp091-go"
)

type Config struct {
	URI   string `env:"RABBIT_URI" env-required:"true"`
	Queue string `env:"RABBIT_QUEUE" env-required:"true"`
}

type Sender interface {
	Send(ctx context.Context, message models.Event) error
}

type Consumer struct {
	Queue      string
	Senders    []Sender
	channel    *amqp.Channel
	Connection *amqp.Connection
}

func NewConsumer(cfg Config, senders []Sender) *Consumer {
	conn, err := amqp.Dial(cfg.URI)
	if err != nil {
		log.Fatalln("Connection error: ", err)
	}
	log.Println("Dial successed: ", cfg.URI)

	channel, err := conn.Channel()
	if err != nil {
		log.Fatalln("Channel error: ", err)
	}
	log.Println("Rabbit channel inited")
	return &Consumer{
		Queue:      cfg.Queue,
		Senders:    senders,
		channel:    channel,
		Connection: conn,
	}
}

func (c *Consumer) Process(ctx context.Context) error {
	msgs, err := c.channel.Consume(c.Queue, "", false, false, false, false, nil)
	if err != nil {
		return err
	}
	log.Println("Starting consuming")
loop:
	for {
		select {
		case msg, ok := <-msgs:
			if !ok {
				return nil
			}
			var message models.Event

			if err := json.Unmarshal(msg.Body, &message); err != nil {
				msg.Nack(false, true)
				continue loop
			}
			// TODO:
			// if all senders failed -> Nack
			for _, sender := range c.Senders {
				if err := sender.Send(ctx, message); err != nil {
					log.Println("sender error: ", err)
					continue
				}
			}
			msg.Ack(false)

		case <-ctx.Done():
			break loop
		}

	}
	log.Println("Rabbit loop stopped")
	return nil
}

func (c *Consumer) Close() error {
	if err := c.channel.Close(); err != nil {
		return err
	}

	return c.Connection.Close()
}
