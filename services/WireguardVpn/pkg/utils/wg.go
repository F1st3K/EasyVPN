package utils

import (
	"fmt"
	"log"
	"os"
	"os/exec"
	"strings"
	"wireguardvpn/pkg/entities"
)

const (
	Wg               = "wg"
	WgDir            = "/etc/wireguard"
	WgConfigPath     = WgDir + "/" + Wg + ".conf"
	ServerPrivateKey = WgDir + "/privatekey"
	ServerPublicKey  = WgDir + "/publickey"
)

type WgManager struct {
	Address string
	Port    string
}

func NewWgManager(address string, port string, initClients []entities.Client) *WgManager {
	wg := WgManager{Port: port, Address: address}

	_, err := os.Stat(WgDir)
	if os.IsNotExist(err) {
		log.Fatalf("Failed to create wg manager: %s", err.Error())
	}

	_, privErr := os.Stat(ServerPrivateKey)
	_, pubErr := os.Stat(ServerPublicKey)
	if os.IsNotExist(privErr) || os.IsNotExist(pubErr) {
		private, public, err := wg.CreateKeys()
		if err != nil {
			log.Fatalf("Failed to create keys: %v", err)
		}
		f, _ := os.OpenFile(ServerPrivateKey, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(private)
		f, _ = os.OpenFile(ServerPublicKey, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(public)

		defer f.Close()
	}

	err = wg.BuildServerConfiguration(initClients)
	if err != nil {
		log.Fatalf("Failed to build server configuration: %v", err)
	}
	tryExecCommand("wg-quick up " + Wg)

	return &wg
}

func (wg WgManager) CreateKeys() (privateKey string, publicKey string, err error) {
	tryExecCommand("ls")
	tryExecCommand("umask 077")
	private_public := tryExecCommand(`privatekey=$(wg genkey) && echo -n "$privatekey;" && wg pubkey <<< "$privatekey"`)

	keys := strings.Split(strings.TrimSpace(private_public), ";")

	if len(keys) != 2 {
		return "", "", fmt.Errorf("failed to parse keys")
	}

	return keys[0], keys[1], nil
}

func (wg WgManager) BuildServerConfiguration(clients []entities.Client) error {
	private, err := os.ReadFile(ServerPrivateKey)
	if err != nil {
		return err
	}

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

	err = os.WriteFile(WgConfigPath, []byte(serverConf), os.ModeAppend)
	if err != nil {
		return err
	}

	return nil
}

func (wg WgManager) GetClientConfiguration(client entities.Client) (string, error) {
	public, err := os.ReadFile(ServerPublicKey)
	if err != nil {
		return "", err
	}

	config := fmt.Sprintf(`
[Interface]
PrivateKey = %s
Address = %s
DNS = 1.1.1

[Peer]
PublicKey = %s
Endpoint = %s:%s
AllowedIPs = 0.0.0.0/0
`, client.PrivateKey, client.Address, string(public), wg.Address, wg.Port)

	return config, nil
}

func (wg WgManager) Restart() error {
	output := tryExecCommand("wg syncconf " + Wg + " <(wg-quick strip " + Wg + ")")
	if output == "" {
		return nil
	}
	return nil // For now, restart always succeeds
}

func tryExecCommand(command string) string {
	cmd := exec.Command("bash", "-c", command)
	out, err := cmd.Output()
	if err != nil {
		log.Printf("Error on exec command: %s: %v", command, err)
	}

	return string(out)
}
