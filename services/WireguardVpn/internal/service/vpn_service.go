package service

import (
	"context"
	"log"
	"net/http"

	"wireguardvpn/pkg/config"
	"wireguardvpn/pkg/entities"
	"wireguardvpn/pkg/interfaces"

	"github.com/gin-gonic/gin"
)

// Auth is a helper struct for basic authentication
type Auth struct {
	Username string
	Password string
}

// CheckBasicAuth validates basic authentication
func (a *Auth) CheckBasicAuth(c *gin.Context) {
	username, password, ok := c.Request.BasicAuth()
	if !ok || username != a.Username || password != a.Password {
		c.AbortWithStatusJSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		return
	}
}

// VPNService represents the main VPN service
type VPNService struct {
	config         *config.Config
	clientRepo     interfaces.ClientRepository
	wgManager      interfaces.WgManager
	addressManager interfaces.AddressManager
	httpServer     *http.Server
}

// NewVPNService creates a new VPN service instance
func NewVPNService(
	cfg *config.Config,
	clientRepo interfaces.ClientRepository,
	wgManager interfaces.WgManager,
	addressManager interfaces.AddressManager,
) *VPNService {
	return &VPNService{
		config:         cfg,
		clientRepo:     clientRepo,
		wgManager:      wgManager,
		addressManager: addressManager,
	}
}

// Start initializes and starts the VPN service
func (s *VPNService) Start() error {
	// Initialize WireGuard configuration with existing clients
	clients, err := s.clientRepo.GetAllClients()
	if err != nil {
		return err
	}

	err = s.wgManager.BuildServerConfiguration(clients)
	if err != nil {
		return err
	}

	// Start HTTP server
	handler := s.initHTTPHandlers()
	s.httpServer = &http.Server{
		Addr:    ":" + s.config.Api.Port,
		Handler: handler,
	}

	log.Printf("WireguardVpn API HTTP server starting on port: %s", s.config.Api.Port)
	if err := s.httpServer.ListenAndServe(); err != nil && err != http.ErrServerClosed {
		return err
	}

	return nil
}

// Stop gracefully stops the VPN service
func (s *VPNService) Stop(ctx context.Context) error {
	if s.httpServer != nil {
		return s.httpServer.Shutdown(ctx)
	}
	return nil
}

// initHTTPHandlers sets up the HTTP routes
func (s *VPNService) initHTTPHandlers() *gin.Engine {
	gin.SetMode(gin.ReleaseMode)
	router := gin.New()

	v1 := router.Group("/v1")
	{
		v1.Use(func(ctx *gin.Context) {
			auth := Auth{Username: s.config.Service.User, Password: s.config.Service.Password}
			auth.CheckBasicAuth(ctx)
		})

		v1.GET("/", func(ctx *gin.Context) { ctx.Status(200) })

		connections := v1.Group("/connections")
		{
			connections.GET("/:id/config", s.GetConnectionConfig)
			connections.POST(".", s.CreateConnection)
			connections.PUT("/:id/enable", s.EnableConnection)
			connections.PUT("/:id/disable", s.DisableConnection)
			connections.DELETE("/:id", s.DeleteConnection)
		}
	}

	return router
}

// GetConnectionConfig retrieves the VPN configuration for a client
func (s *VPNService) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	client, err := s.clientRepo.GetClient(id)
	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": "Client not found"})
		return
	}

	conf, err := s.wgManager.GetClientConfiguration(client)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get client configuration"})
		return
	}

	c.String(200, conf)
}

// CreateConnection creates a new VPN connection
func (s *VPNService) CreateConnection(c *gin.Context) {
	id := c.Query("id")

	private, public, err := s.wgManager.CreateKeys()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create keys"})
		return
	}

	address, err := s.addressManager.GenerateAddress()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to generate address"})
		return
	}

	err = s.clientRepo.CreateClient(entities.Client{
		Id:         id,
		PrivateKey: private,
		PublicKey:  public,
		Address:    address,
		IsEnabled:  false,
	})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create client"})
		return
	}

	c.Status(201)
}

// EnableConnection enables a VPN connection
func (s *VPNService) EnableConnection(c *gin.Context) {
	id := c.Param("id")

	err := s.clientRepo.UpdateClient(
		entities.Client{Id: id, IsEnabled: true})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update client"})
		return
	}

	clients, err := s.clientRepo.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = s.wgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = s.wgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204)
}

// DisableConnection disables a VPN connection
func (s *VPNService) DisableConnection(c *gin.Context) {
	id := c.Param("id")

	err := s.clientRepo.UpdateClient(
		entities.Client{Id: id, IsEnabled: false})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update client"})
		return
	}

	clients, err := s.clientRepo.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = s.wgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = s.wgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204)
}

// DeleteConnection deletes a VPN connection
func (s *VPNService) DeleteConnection(c *gin.Context) {
	id := c.Param("id")

	err := s.clientRepo.RemoveClient(id)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to remove client"})
		return
	}

	clients, err := s.clientRepo.GetAllClients()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get all clients"})
		return
	}

	err = s.wgManager.BuildServerConfiguration(clients)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to build server configuration"})
		return
	}

	err = s.wgManager.Restart()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to restart WireGuard"})
		return
	}

	c.Status(204)
}
