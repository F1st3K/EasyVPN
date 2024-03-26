package wireguardvpn

import (
	"fmt"

	"github.com/gin-gonic/gin"
)

func (h *Handler) getConnectionConfig(c *gin.Context) {
	fmt.Fprintln(c.Writer, "Nicceee")
}
