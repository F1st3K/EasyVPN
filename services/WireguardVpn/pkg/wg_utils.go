package wireguardvpn

import (
	"fmt"
	"log"
	"os"
	"os/exec"
)

const WG = "wg"
const WG_DIR = "/etc/wireguard"
const WG_IPCOUTN = WG_DIR + "/ipcount"
const WG_CONFIG_PATH = WG_DIR + "/" + WG + ".conf"
const CLIENTS = WG_DIR + "/clients"
const DIS_CLIENTS = WG_DIR + "/disabled"
const PRIVATE_KEY = "/privatekey"
const PUBLIC_KEY = "/publickey"
const IP = "/ip"

var Port string

func StartUp(port string) {
	Port = port
	tryCreateKeys(WG_DIR+PRIVATE_KEY, WG_DIR+PUBLIC_KEY)

	buildServerConfiguration()
	wgUp()
}

func tryCreateKeys(privateKey string, publicKey string) {
	_, privErr := os.Stat(privateKey)
	_, pubErr := os.Stat(publicKey)
	if !os.IsNotExist(privErr) && !os.IsNotExist(pubErr) {
		return
	}
	tryExecCommand("ls")
	tryExecCommand("umask 077")
	tryExecCommand(fmt.Sprintf("wg genkey > %s", privateKey))
	tryExecCommand(fmt.Sprintf("wg pubkey < %s > %s", privateKey, publicKey))
	tryExecCommand("chmod 600 " + privateKey)
}

func buildServerConfiguration() {
	private, _ := os.ReadFile(WG_DIR + PRIVATE_KEY)

	serverConf := fmt.Sprintf(`
	[Interface]
	PrivateKey = %s
	Address = 10.0.0.1/24
	ListenPort = %s
	PostUp = iptables -A FORWARD -i %%i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
	PostDown = iptables -D FORWARD -i %%i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE
	
	`, string(private), Port)

	clients, _ := os.ReadDir(CLIENTS)
	for i := 0; i < len(clients); i++ {
		if !clients[i].IsDir() {
			continue
		}

		client := CLIENTS + "/" + clients[i].Name()
		public, _ := os.ReadFile(client + PUBLIC_KEY)
		ip, _ := os.ReadFile(client + IP)

		serverConf += fmt.Sprintf(`#%s
		[Peer]
		PublicKey = %s
		AllowedIPs = %s
		
		`, client, string(public), string(ip))
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

func tryExecCommand(command string) {
	err := exec.Command("bash", "-c", command).Run()
	if err != nil {
		log.Printf("Error on exec command: %s: %v", command, err)
	}
}
