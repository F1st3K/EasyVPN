package interfaces

import (
	"wireguardvpn/pkg/entities"
)

// ClientRepository interface defines the contract for client data operations
type ClientRepository interface {
	GetClient(id string) (entities.Client, error)
	GetAllClients() ([]entities.Client, error)
	CreateClient(client entities.Client) error
	UpdateClient(client entities.Client) error
	RemoveClient(id string) error
}
