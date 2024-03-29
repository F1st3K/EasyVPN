package wireguardvpn

import (
	"strings"

	"github.com/gin-gonic/gin"
)

func (h *Handler) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	c.String(200, "configuraiton -> "+id) // OK
	//c.JSON(200, "nicceess "+u+p)
	//fmt.Fprintln(c.Writer, "Nicceee")
}

func (h *Handler) CreateConnection(c *gin.Context) {
	id := c.Query("id")

	CreateClient(id)

	c.Status(201) // Created
}

func (h *Handler) EnableConnection(c *gin.Context) {
	id := c.Param("id")
	strings.Split(id, "")

	c.Status(204) // No Content
}

func (h *Handler) DisableConnection(c *gin.Context) {
	id := c.Param("id")
	strings.Split(id, "")

	c.Status(204) // No Content
}

func (h *Handler) DeleteConnection(c *gin.Context) {
	id := c.Param("id")
	strings.Split(id, "")

	c.Status(204) // No Content
}
