package repositories

import (
	"log"
	"os"
	"strings"
	"wireguardvpn/pkg/entities"
)

const (
	PUBLIC_KEY  = "/publickey"
	PRIVATE_KEY = "/privatekey"
	IP          = "/ip"
	ENABLED     = "/enabled"
)

type ClientRepository struct {
	BasePath string
}

func NewClientRepository(basePath string) *ClientRepository {
	r := ClientRepository{BasePath: basePath}

	return &r
}

func (r ClientRepository) GetClient(id string) (entities.Client, error) {
	client := r.BasePath + "/" + id
	s, err := os.Stat(client)
	if os.IsNotExist(err) || !s.IsDir() {
		log.Println("ClientRepository: get: client not found: " + client)
		return entities.Client{}, err
	}

	public, err := os.ReadFile(client + PUBLIC_KEY)
	if err != nil {
		return entities.Client{}, err
	}
	private, err := os.ReadFile(client + PRIVATE_KEY)
	if err != nil {
		return entities.Client{}, err
	}
	ip, err := os.ReadFile(client + IP)
	if err != nil {
		return entities.Client{}, err
	}
	enabled, err := os.ReadFile(client + ENABLED)
	if err != nil {
		return entities.Client{}, err
	}

	var isEnabled bool
	if string(enabled) == "1" {
		isEnabled = true
	} else {
		isEnabled = false
	}

	return entities.Client{
		Id:         id,
		PublicKey:  strings.TrimSpace(string(public)),
		PrivateKey: strings.TrimSpace(string(private)),
		Address:    strings.TrimSpace(string(ip)),
		IsEnabled:  isEnabled,
	}, nil
}

func (r ClientRepository) GetAllClients() ([]entities.Client, error) {
	var clients []entities.Client

	clientIds, err := os.ReadDir(r.BasePath)
	if err != nil {
		return nil, err
	}

	for i := 0; i < len(clientIds); i++ {
		if !clientIds[i].IsDir() {
			continue
		}

		c, err := r.GetClient(clientIds[i].Name())
		if err != nil {
			log.Printf("Error getting client %s: %v", clientIds[i].Name(), err)
			continue
		}
		clients = append(clients, c)
	}

	return clients, nil
}

func (r ClientRepository) CreateClient(client entities.Client) error {
	clientPath := r.BasePath + "/" + client.Id
	err := os.MkdirAll(clientPath, os.ModeAppend)
	if err != nil {
		return err
	}

	f, err := os.Create(clientPath + PUBLIC_KEY)
	if err != nil {
		return err
	}
	defer f.Close()

	_, err = f.WriteString(client.PublicKey)
	if err != nil {
		return err
	}

	f, err = os.Create(clientPath + PRIVATE_KEY)
	if err != nil {
		return err
	}
	defer f.Close()
	_, err = f.WriteString(client.PrivateKey)
	if err != nil {
		return err
	}

	f, err = os.Create(clientPath + IP)
	if err != nil {
		return err
	}
	defer f.Close()
	_, err = f.WriteString(client.Address)
	if err != nil {
		return err
	}

	f, err = os.Create(clientPath + ENABLED)
	if err != nil {
		return err
	}
	defer f.Close()

	var enabled string
	if client.IsEnabled {
		enabled = "1"
	} else {
		enabled = "0"
	}
	_, err = f.WriteString(enabled)
	if err != nil {
		return err
	}

	return nil
}

func (r ClientRepository) UpdateClient(client entities.Client) error {
	clientPath := r.BasePath + "/" + client.Id
	_, err := os.Stat(clientPath)
	if os.IsNotExist(err) {
		log.Println("ClientRepository: update: client not found: " + clientPath)
		return err
	}

	if client.PublicKey != "" {
		f, err := os.OpenFile(clientPath+PUBLIC_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		if err != nil {
			return err
		}
		defer f.Close()
		_, err = f.WriteString(client.PublicKey)
		if err != nil {
			return err
		}
	}
	if client.PrivateKey != "" {
		f, err := os.OpenFile(clientPath+PRIVATE_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		if err != nil {
			return err
		}
		defer f.Close()
		_, err = f.WriteString(client.PrivateKey)
		if err != nil {
			return err
		}
	}
	if client.Address != "" {
		f, err := os.OpenFile(clientPath+IP, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		if err != nil {
			return err
		}
		defer f.Close()
		_, err = f.WriteString(client.Address)
		if err != nil {
			return err
		}
	}

	f, err := os.OpenFile(clientPath+ENABLED, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
	if err != nil {
		return err
	}
	defer f.Close()

	var enabled string
	if client.IsEnabled {
		enabled = "1"
	} else {
		enabled = "0"
	}
	_, err = f.WriteString(enabled)
	if err != nil {
		return err
	}

	return nil
}

func (r ClientRepository) RemoveClient(id string) error {
	clientPath := r.BasePath + "/" + id
	err := os.RemoveAll(clientPath)
	if os.IsNotExist(err) {
		log.Println("ClientRepository: remove: client not found: " + clientPath)
		return nil // Don't return error if client doesn't exist
	}
	return err
}
