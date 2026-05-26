package wireguardvpn

import (
	"context"
	"log"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
)

// Server represents the HTTP server
type Server struct {
	httpServer *http.Server
}

// Run starts the HTTP server
func (s *Server) Run(port string, handler *gin.Engine) error {
	s.httpServer = &http.Server{
		Addr:    ":" + port,
		Handler: handler,
	}

	log.Printf("Starting server on port %s", port)
	if err := s.httpServer.ListenAndServe(); err != nil && err != http.ErrServerClosed {
		return err
	}

	return nil
}

// Stop gracefully shuts down the server
func (s *Server) Stop(ctx context.Context) error {
	if s.httpServer != nil {
		ctxWithTimeout, cancel := context.WithTimeout(context.Background(), 5*time.Second)
		defer cancel()
		return s.httpServer.Shutdown(ctxWithTimeout)
	}
	return nil
}
