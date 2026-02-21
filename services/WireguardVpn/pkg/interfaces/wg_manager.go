package interfaces

import (
	"wireguardvpn/pkg/entities"
)

// WgManager interface defines the contract for WireGuard management operations
type WgManager interface {
	CreateKeys() (privateKey string, publicKey string, err error)
	BuildServerConfiguration(clients []entities.Client) error
	GetClientConfiguration(client entities.Client) (string, error)
	Restart() error
}
