package wireguardvpn

import (
	"github.com/gin-gonic/gin"
)

func (h *Handler) getConnectionConfig(c *gin.Context) {
	u, p, _ := c.Request.BasicAuth()
	c.JSON(200, "nicceess "+u+p)
	//fmt.Fprintln(c.Writer, "Nicceee")
}
