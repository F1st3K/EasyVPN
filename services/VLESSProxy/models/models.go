package models

import "github.com/google/uuid"

type Connection struct {
	ID      string
	UUID    uuid.UUID
	Enabled bool
}
