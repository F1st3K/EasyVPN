package wireguardvpn

import (
	"WireguardVpn/pkg/repositories"
	"WireguardVpn/pkg/utils"

	"github.com/gin-gonic/gin"
)

type Handler struct {
	ClientRepository repositories.ClientRepository
	WgManager        utils.WgManager
	AddressManager   utils.AddressManager
}

func NewHandler(
	clientRep repositories.ClientRepository,
	wg utils.WgManager,
	addrs utils.AddressManager) *Handler {
	return &Handler{
		ClientRepository: clientRep,
		WgManager:        wg,
		AddressManager:   addrs,
	}
}

func (h *Handler) InitRoutes(user string, password string) *gin.Engine {
	gin.SetMode(gin.ReleaseMode)
	router := gin.New()

	v1 := router.Group("/v1")
	{
		v1.Use(func(ctx *gin.Context) {
			auth := Auth{Username: user, Password: password}
			auth.CheckBasicAuth(ctx)
		})

		v1.GET("/", func(ctx *gin.Context) { ctx.Status(200) })

		connections := v1.Group("/connections")
		{
			connections.GET("/:id/config", h.GetConnectionConfig)

			connections.POST(".", h.CreateConnection)

			connections.PUT("/:id/enable", h.EnableConnection)

			connections.PUT("/:id/disable", h.DisableConnection)

			connections.DELETE("/:id", h.DeleteConnection)
		}
	}

	return router
}
