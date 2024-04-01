package wireguardvpn

import (
	"WireguardVpn/pkg/entities"

	"github.com/gin-gonic/gin"
)

func (h *Handler) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	client := h.ClientRepository.GetClient(id)
	conf := h.WgManager.GetClientConfiguration(client)

	c.String(200, conf) // OK
}

func (h *Handler) CreateConnection(c *gin.Context) {
	id := c.Query("id")

	private, public := h.WgManager.CreateKeys()
	address := h.AddressManager.GetNextAllowedIp()

	h.ClientRepository.CreateClient(entities.Client{
		Id:         id,
		PrivateKey: private,
		PublicKey:  public,
		Address:    address,
		IsEnabled:  false,
	})

	c.Status(201) // Created
}

func (h *Handler) EnableConnection(c *gin.Context) {
	id := c.Param("id")

	h.ClientRepository.UpdateClient(
		entities.Client{Id: id, IsEnabled: true})

	h.WgManager.BuildServerConfiguration(
		h.ClientRepository.GetAllClients())
	h.WgManager.Restart()

	c.Status(204) // No Content
}

func (h *Handler) DisableConnection(c *gin.Context) {
	id := c.Param("id")

	h.ClientRepository.UpdateClient(
		entities.Client{Id: id, IsEnabled: false})

	h.WgManager.BuildServerConfiguration(
		h.ClientRepository.GetAllClients())
	h.WgManager.Restart()

	c.Status(204) // No Content
}

func (h *Handler) DeleteConnection(c *gin.Context) {
	id := c.Param("id")

	h.ClientRepository.RemoveClient(id)

	h.WgManager.BuildServerConfiguration(
		h.ClientRepository.GetAllClients())
	h.WgManager.Restart()

	c.Status(204) // No Content
}
