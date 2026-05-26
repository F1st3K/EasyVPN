package api

import "github.com/gin-gonic/gin"

func (h *Handler) InitRoutes() *gin.Engine {
	gin.SetMode(gin.ReleaseMode)
	router := gin.New()

	v1 := router.Group("/v1")
	{
		v1.Use(func(ctx *gin.Context) {
			h.auth.CheckBasicAuth(ctx)
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
