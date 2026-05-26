package api

import (
	"VlessProxy/service"

	"github.com/gin-gonic/gin"
)

type Handler struct {
	srv  *service.Service
	auth *Auth
}

func NewHandler(srv *service.Service, auth *Auth) *Handler {
	return &Handler{
		srv:  srv,
		auth: auth,
	}
}

func (h *Handler) CreateConnection(c *gin.Context) {
	id := c.Query("id")

	err := h.srv.CreateNewConnection(c.Request.Context(), id)
	if err != nil {
		c.JSON(500, err.Error())
		return
	}
	c.JSON(201, gin.H{
		"status": "ok",
	})
}

func (h *Handler) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	cfg, err := h.srv.GetConfig(c.Request.Context(), id)
	if err != nil {
		c.JSON(500, err.Error())
		return
	}
	c.JSON(200, cfg)
}

func (h *Handler) DeleteConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.srv.DeleteConnection(c.Request.Context(), id)
	if err != nil {
		c.JSON(500, err.Error())
		return
	}
	c.JSON(204, gin.H{
		"status": "ok",
	})
}

func (h *Handler) EnableConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.srv.ActivateConnection(c.Request.Context(), id)
	if err != nil {
		c.JSON(500, err.Error())
		return
	}
	c.JSON(204, gin.H{
		"status": "ok",
	})
}

func (h *Handler) DisableConnection(c *gin.Context) {
	id := c.Param("id")

	err := h.srv.DeactivateConnection(c.Request.Context(), id)
	if err != nil {
		c.JSON(500, err.Error())
		return
	}
	c.JSON(204, gin.H{
		"status": "ok",
	})
}
