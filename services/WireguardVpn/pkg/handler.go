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

	router.GET("/", func(ctx *gin.Context) { ctx.Status(200) })

	connections := router.Group("/connections")
	{
		connections.GET("/:id/config", h.GetConnectionConfig)

		connections.POST("", h.CreateConnection)

		connections.PUT("/:id/enable", h.EnableConnection)

		connections.PUT("/:id/disable", h.DisableConnection)

		connections.DELETE("/:id", h.DeleteConnection)
	}

	return router
}
