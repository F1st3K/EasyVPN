package wireguardvpn

import (
	"net/http"

	"wireguardvpn/pkg/entities"

	"github.com/gin-gonic/gin"
)

func (h *Handler) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	client, err := h.ClientRepository.GetClient(id)
	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": "Client not found"})
		return
	}

	conf, err := h.WgManager.GetClientConfiguration(client)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get client configuration"})
		return
	}

	c.String(200, conf) // OK
}

func (h *Handler) CreateConnection(c *gin.Context) {
	id := c.Query("id")

	private, public, err := h.WgManager.CreateKeys()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create keys"})
		return
	}

	address, err := h.AddressManager.GenerateAddress()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to generate address"})
		return
	}

	err = h.ClientRepository.CreateClient(entities.Client{
		Id:         id,
		PrivateKey: private,
		PublicKey:  public,
		Address:    address,
		IsEnabled:  false,
	})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create client"})
		return
	}

	c.Status(201) // Created
}

func (h *Handler) EnableConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.ClientRepository.UpdateClient(
		entities.Client{Id: id, IsEnabled: true})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update client"})
		return
	}

	clients, err := h.ClientRepository.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = h.WgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = h.WgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204) // No Content
}

func (h *Handler) DisableConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.ClientRepository.UpdateClient(
		entities.Client{Id: id, IsEnabled: false})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update client"})
		return
	}

	clients, err := h.ClientRepository.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = h.WgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = h.WgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204) // No Content
}

func (h *Handler) DeleteConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.ClientRepository.RemoveClient(id)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to remove client"})
		return
	}

	clients, err := h.ClientRepository.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = h.WgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = h.WgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204) // No Content
}
