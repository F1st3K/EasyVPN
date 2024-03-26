package wireguardvpn

import (
	"github.com/gin-gonic/gin"
)

type Handler struct {
}

func (h *Handler) InitRoutes() *gin.Engine {
	router := gin.New()

	connections := router.Group("/connections")
	{
		connections.GET("/config", h.getConnectionConfig)
	}

	return router
}
