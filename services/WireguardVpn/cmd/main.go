package main

import (
	wireguardvpn "WireguardVpn"
	"log"
)

func main() {
	port := "8080"
	srv := new(wireguardvpn.Server)

	log.Printf("WireguardVpn api http server started on port: %s", port)
	if err := srv.Run(port); err != nil {
		log.Fatalf("Error while ocurated http server: %s", err.Error())
	}

}
