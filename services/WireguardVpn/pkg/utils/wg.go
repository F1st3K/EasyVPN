package utils

import (
	"WireguardVpn/pkg/repositories"
	"fmt"
	"log"
	"os"
	"os/exec"
	"strings"
)

const WG = "wg"
const WG_DIR = "/etc/wireguard"
const WG_IPCOUTN = WG_DIR + "/ipcount"
const WG_CONFIG_PATH = WG_DIR + "/" + WG + ".conf"
const CLIENTS = WG_DIR + "/clients"
const DIS_CLIENTS = WG_DIR + "/disabled"
const SERVER_PRIVATE_KEY = WG_DIR + "/privatekey"
const SERVER_PUBLIC_KEY = WG_DIR + "/publickey"
const IP = "/ip"

type Wg struct {
	Port             string
	ClientRepository repositories.ClientRepository
}

var Port string

func (wg Wg) StartUp() {

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

	wg.BuildServerConfiguration()
	wgUp()
}

func (wg Wg) CreateKeys() (privateKey string, publicKey string) {
	tryExecCommand("ls")
	tryExecCommand("umask 077")
	private_public := tryExecCommand(`privatekey=$(wg genkey) && echo -n "$privatekey;" && wg pubkey <<< "$privatekey"`)

	keys := strings.Split(private_public, ";")

	return keys[0], keys[1]
}

func (wg Wg) BuildServerConfiguration() {
	private, _ := os.ReadFile(SERVER_PRIVATE_KEY)

	serverConf := fmt.Sprintf(`
	[Interface]
	PrivateKey = %s
	Address = 10.0.0.1/24
	ListenPort = %s
	PostUp = iptables -A FORWARD -i %%i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
	PostDown = iptables -D FORWARD -i %%i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE
	
	`, string(private), Port)

	clients := wg.ClientRepository.GetAllClients()
	for i := 0; i < len(clients); i++ {
		client := clients[i]
		serverConf += fmt.Sprintf(`#%s
		[Peer]
		PublicKey = %s
		AllowedIPs = %s
		
		`, client.Id, client.PublicKey, client.AllowedIPs)
	}

	os.WriteFile(WG_CONFIG_PATH, []byte(serverConf), os.ModeAppend)
}

func wgUp() {
	tryExecCommand("wg-quick up " + WG)
}

func wgDown() {
	tryExecCommand("wg-quick down " + WG)
}

func getNextAllowedIp() string {
	return "10.0.0.2/32"
}

func tryExecCommand(command string) string {
	cmd := exec.Command("bash", "-c", command)
	out, err := cmd.Output()
	if err != nil {
		log.Printf("Error on exec command: %s: %v", command, err)
	}

	return string(out)
}
