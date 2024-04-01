package repositories

import (
	"WireguardVpn/pkg/entities"
	"log"
	"os"
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

func NewClientRepoitory(basePath string) *ClientRepository {
	r := ClientRepository{BasePath: basePath}

	return &r
}

func (r ClientRepository) GetClient(id string) entities.Client {
	client := r.BasePath + "/" + id
	s, err := os.Stat(client)
	if os.IsNotExist(err) || !s.IsDir() {
		log.Println("ClientRepository: get: client not found: " + client)
		return entities.Client{}
	}

	public, _ := os.ReadFile(client + PUBLIC_KEY)
	private, _ := os.ReadFile(client + PRIVATE_KEY)
	ip, _ := os.ReadFile(client + IP)
	enabled, _ := os.ReadFile(client + ENABLED)

	var isEnabled bool
	if string(enabled) == "1" {
		isEnabled = true
	} else {
		isEnabled = false
	}

	return entities.Client{
		Id:         id,
		PublicKey:  string(public),
		PrivateKey: string(private),
		Address:    string(ip),
		IsEnabled:  isEnabled,
	}
}

func (r ClientRepository) GetAllClients() []entities.Client {
	var clients []entities.Client

	clientIds, _ := os.ReadDir(r.BasePath)
	for i := 0; i < len(clientIds); i++ {
		if !clientIds[i].IsDir() {
			continue
		}

		c := r.GetClient(clientIds[i].Name())
		clients = append(clients, c)
	}

	return clients
}

func (r ClientRepository) CreateClient(client entities.Client) {
	clientPath := r.BasePath + "/" + client.Id
	err := os.MkdirAll(clientPath, os.ModeAppend)
	if os.IsExist(err) {
		log.Println("ClientRepository: create: client already exist: " + clientPath)
	}

	f, _ := os.Create(clientPath + PUBLIC_KEY)
	f.WriteString(client.PublicKey)
	f, _ = os.Create(clientPath + PRIVATE_KEY)
	f.WriteString(client.PrivateKey)
	f, _ = os.Create(clientPath + IP)
	f.WriteString(client.Address)
	f, _ = os.Create(clientPath + ENABLED)
	var enabled string
	if client.IsEnabled {
		enabled = "1"
	} else {
		enabled = "0"
	}
	f.WriteString(enabled)

	defer f.Close()
}

func (r ClientRepository) UpdateClient(client entities.Client) {
	clientPath := r.BasePath + "/" + client.Id
	_, err := os.Stat(clientPath)
	if os.IsNotExist(err) {
		log.Println("ClientRepository: update: client not found: " + clientPath)
	}

	var f *os.File
	if client.PublicKey != "" {
		f, _ = os.OpenFile(clientPath+PUBLIC_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(client.PublicKey)
	}
	if client.PrivateKey != "" {
		f, _ = os.OpenFile(clientPath+PRIVATE_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(client.PrivateKey)
	}
	if client.Address != "" {
		f, _ = os.OpenFile(clientPath+IP, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(client.Address)
	}
	f, _ = os.OpenFile(clientPath+ENABLED, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
	var enabled string
	if client.IsEnabled {
		enabled = "1"
	} else {
		enabled = "0"
	}
	f.WriteString(enabled)

	defer f.Close()
}

func (r ClientRepository) RemoveClient(id string) {
	clientPath := r.BasePath + "/" + id
	err := os.RemoveAll(clientPath)
	if os.IsNotExist(err) {
		log.Println("ClientRepository: remove: client not found: " + clientPath)
	}
}
