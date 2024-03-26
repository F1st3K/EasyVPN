package wireguardvpn

import (
	"github.com/gin-gonic/gin"
)

type Handler struct {
}

func (h *Handler) InitRoutes() *gin.Engine {
	router := gin.New()

	client := router.Group("/connection")
	{
		client.GET("/config", h.getConnectionConfig)
	}

	return router
}
