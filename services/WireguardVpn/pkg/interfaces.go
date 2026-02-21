package wireguardvpn

import (
	"wireguardvpn/pkg/entities"

	"github.com/gin-gonic/gin"
)

// ClientRepository interface defines the contract for client data operations
type ClientRepository interface {
	GetClient(id string) (entities.Client, error)
	GetAllClients() ([]entities.Client, error)
	CreateClient(client entities.Client) error
	UpdateClient(client entities.Client) error
	RemoveClient(id string) error
}

// WgManager interface defines the contract for WireGuard management operations
type WgManager interface {
	CreateKeys() (privateKey string, publicKey string, err error)
	BuildServerConfiguration(clients []entities.Client) error
	GetClientConfiguration(client entities.Client) (string, error)
	Restart() error
}

// AddressManager interface defines the contract for address management operations
type AddressManager interface {
	GenerateAddress() (string, error)
	ValidateAddress(address string) error
}

// Service interface defines the contract for the main service operations
type Service interface {
	Start() error
	Stop() error
	Restart() error
}

// HTTPHandler interface defines the contract for HTTP handlers
type HTTPHandler interface {
	InitRoutes() *gin.Engine
}
