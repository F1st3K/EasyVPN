package wireguardvpn

import (
	wireguardvpn "WireguardVpn/pkg"
	"context"
	"net/http"
	"time"
)

type Server struct {
	httpServer *http.Server
}

func (s *Server) Run(port string) error {
	handler := new(wireguardvpn.Handler).InitRoutes()

	s.httpServer = &http.Server{
		Addr:           ":" + port,
		Handler:        handler,
		MaxHeaderBytes: 1 << 20, //1MB
		ReadTimeout:    10 * time.Second,
		WriteTimeout:   10 * time.Second,
	}

	return s.httpServer.ListenAndServe()
}

func (s *Server) Shutdown(c context.Context) error {
	return s.httpServer.Shutdown(c)
}
