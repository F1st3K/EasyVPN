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

func StartUp(port string) {
	tryCreateKeys(WG_DIR+PRIVATE_KEY, WG_DIR+PUBLIC_KEY)

	buildServerConfiguration(port)
	wgUp()
}

func CreateClient(uname string) {
	client := DIS_CLIENTS + "/" + uname
	_ = os.MkdirAll(client, os.ModeAppend)

	tryCreateKeys(client+PRIVATE_KEY, client+PUBLIC_KEY)

	_, err := os.Stat(client + IP)
	if os.IsNotExist(err) {
		f, _ := os.Create(client + IP)
		f.WriteString(getNextAllowedIp())
		f.Close()
	}
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

func buildServerConfiguration(port string) {
	private, _ := os.ReadFile(WG_DIR + PRIVATE_KEY)

	serverConf := fmt.Sprintf(`
	[Interface]
	PrivateKey = %s
	Address = 10.0.0.1/24
	ListenPort = %s
	PostUp = iptables -A FORWARD -i %%i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
	PostDown = iptables -D FORWARD -i %%i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE
	`, string(private), port)

	clients, _ := os.ReadDir(CLIENTS)
	for i := 0; i < len(clients); i++ {
		if !clients[i].IsDir() {
			continue
		}

		client := clients[i].Name()
		public, _ := os.ReadFile(client + PUBLIC_KEY)
		ip, _ := os.ReadFile(client + IP)

		serverConf += fmt.Sprintf(`
		[Peer]
		PublicKey = %s
		AllowedIPs = %s
		#%s`, string(public), string(ip), client)
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
