package wireguardvpn

import (
	"github.com/gin-gonic/gin"
)

type Handler struct {
}

func (h *Handler) InitRoutes(user string, password string) *gin.Engine {
	router := gin.New()

	router.Use(func(ctx *gin.Context) {
		auth := Auth{Username: user, Password: password}
		auth.CheckBasicAuth(ctx)
	})

	connections := router.Group("/connections")
	{
		connections.GET("/config", h.getConnectionConfig)
	}

	return router
}
