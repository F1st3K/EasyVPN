package utils

import (
	"WireguardVpn/pkg/entities"
	"fmt"
	"log"
	"os"
	"os/exec"
	"strings"
)

const (
	WG                 = "wg"
	WG_DIR             = "/etc/wireguard"
	WG_CONFIG_PATH     = WG_DIR + "/" + WG + ".conf"
	SERVER_PRIVATE_KEY = WG_DIR + "/privatekey"
	SERVER_PUBLIC_KEY  = WG_DIR + "/publickey"
)

type WgManager struct {
	Address string
	Port    string
}

func NewWgManager(address string, port string, initClients []entities.Client) *WgManager {
	wg := WgManager{Port: port, Address: address}

	_, err := os.Stat(WG_DIR)
	if os.IsNotExist(err) {
		log.Fatalf("Failed to create wg manager: %s", err.Error())
	}

	_, privErr := os.Stat(SERVER_PRIVATE_KEY)
	_, pubErr := os.Stat(SERVER_PUBLIC_KEY)
	if os.IsNotExist(privErr) || os.IsNotExist(pubErr) {
		private, public := wg.CreateKeys()
		f, _ := os.OpenFile(SERVER_PRIVATE_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(private)
		f, _ = os.OpenFile(SERVER_PUBLIC_KEY, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(public)

		defer f.Close()
	}

	wg.BuildServerConfiguration(initClients)
	tryExecCommand("wg-quick up " + WG)

	return &wg
}

func (wg WgManager) CreateKeys() (privateKey string, publicKey string) {
	tryExecCommand("ls")
	tryExecCommand("umask 077")
	private_public := tryExecCommand(`privatekey=$(wg genkey) && echo -n "$privatekey;" && wg pubkey <<< "$privatekey"`)

	keys := strings.Split(private_public, ";")

	return keys[0], keys[1]
}

func (wg WgManager) BuildServerConfiguration(clients []entities.Client) {
	private, _ := os.ReadFile(SERVER_PRIVATE_KEY)

	serverConf := fmt.Sprintf(`[Interface]
PrivateKey = %s
Address = 10.0.0.1/24
ListenPort = %s
PostUp = iptables -A FORWARD -i %%i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
PostDown = iptables -D FORWARD -i %%i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE

`, string(private), wg.Port)

	for i := 0; i < len(clients); i++ {
		client := clients[i]
		if !client.IsEnabled {
			continue
		}
		serverConf += fmt.Sprintf(`#%s
[Peer]
PublicKey = %s
AllowedIPs = %s

`, client.Id, client.PublicKey, client.Address)
	}

	os.WriteFile(WG_CONFIG_PATH, []byte(serverConf), os.ModeAppend)
}

func (wg WgManager) GetClientConfiguration(client entities.Client) string {
	public, _ := os.ReadFile(SERVER_PUBLIC_KEY)

	return fmt.Sprintf(`
[Interface]
PrivateKey = %s
Address = %s
DNS = 1.1.1.1

[Peer]
PublicKey = %s
Endpoint = %s:%s
AllowedIPs = 0.0.0.0/0
`, client.PrivateKey, client.Address, string(public), wg.Address, wg.Port)
}

func (wg WgManager) Restart() {
	tryExecCommand("wg-quick down " + WG)
	tryExecCommand("wg-quick up " + WG)
}

func tryExecCommand(command string) string {
	cmd := exec.Command("bash", "-c", command)
	out, err := cmd.Output()
	if err != nil {
		log.Printf("Error on exec command: %s: %v", command, err)
	}

	return string(out)
}
