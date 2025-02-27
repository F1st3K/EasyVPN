package utils

import (
	"WireguardVpn/pkg/entities"
	"fmt"
	"log"
	"math/rand"
	"os"
	"os/exec"
	"strings"
)

const (
	Wg               = "wg"
	WgDir            = "/etc/amnezia/amneziawg"
	WgConfigPath     = WgDir + "/" + Wg + ".conf"
	ServerPrivateKey = WgDir + "/privatekey"
	ServerPublicKey  = WgDir + "/publickey"
	AmneziaSettings  = WgDir + "/amnezia.settings"
)

type WgManager struct {
	Address string
	Port    string
}

func GetRandSettings() string {
	Jc := rand.Intn(127-3) + 3
	Jmin := rand.Intn(700-3) + 3
	Jmax := rand.Intn(1270-Jmin+1) + Jmin + 1
	return fmt.Sprintf(`Jc = %d
Jmin = %d
Jmax = %d`,
		Jc, Jmin, Jmax)
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
		private, public := wg.CreateKeys()
		f, _ := os.OpenFile(ServerPrivateKey, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(private)
		f, _ = os.OpenFile(ServerPublicKey, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(public)

		defer f.Close()
	}

	_, asErr := os.Stat(AmneziaSettings)
	if os.IsNotExist(asErr) {

		S1 := rand.Intn(127-3) + 3
		S2 := rand.Intn(127-3) + 3
		H1 := rand.Intn(2147483648-268435505) + 268435505
		H2 := rand.Intn(2147483648-268435505) + 268435505
		H3 := rand.Intn(2147483648-268435505) + 268435505
		H4 := rand.Intn(2147483648-268435505) + 268435505

		settings := fmt.Sprintf(`S1 = %d
S2 = %d
H1 = %d
H2 = %d
H3 = %d
H4 = %d`,
			S1, S2, H1, H2, H3, H4)
		f, _ := os.OpenFile(AmneziaSettings, os.O_WRONLY|os.O_CREATE, os.ModeAppend)
		f.WriteString(settings)
		defer f.Close()
	}

	wg.BuildServerConfiguration(initClients)
	tryExecCommand("awg-quick up " + Wg)

	return &wg
}

func (wg WgManager) CreateKeys() (privateKey string, publicKey string) {
	tryExecCommand("ls")
	tryExecCommand("umask 077")
	private_public := tryExecCommand(`privatekey=$(awg genkey) && echo -n "$privatekey;" && awg pubkey <<< "$privatekey"`)

	keys := strings.Split(strings.TrimSpace(private_public), ";")

	return keys[0], keys[1]
}

func (wg WgManager) BuildServerConfiguration(clients []entities.Client) {
	private, _ := os.ReadFile(ServerPrivateKey)
	awgSets, _ := os.ReadFile(AmneziaSettings)

	serverConf := fmt.Sprintf(`[Interface]
PrivateKey = %s
Address = 10.0.0.1/24
ListenPort = %s
PostUp = iptables -A FORWARD -i %%i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
PostDown = iptables -D FORWARD -i %%i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE
%s
%s

`, string(private), wg.Port, GetRandSettings(), awgSets)

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

	os.WriteFile(WgConfigPath, []byte(serverConf), os.ModeAppend)
}

func (wg WgManager) GetClientConfiguration(client entities.Client) string {
	public, _ := os.ReadFile(ServerPublicKey)
	awgSets, _ := os.ReadFile(AmneziaSettings)

	return fmt.Sprintf(`
[Interface]
PrivateKey = %s
Address = %s
DNS = 1.1.1.1
%s
%s

[Peer]
PublicKey = %s
Endpoint = %s:%s
AllowedIPs = 0.0.0.0/0
`, client.PrivateKey, client.Address, GetRandSettings(), awgSets, string(public), wg.Address, wg.Port)
}

func (wg WgManager) Restart() {
	tryExecCommand("awg syncconf " + Wg + " <(awg-quick strip " + Wg + ")")
}

func tryExecCommand(command string) string {
	cmd := exec.Command("bash", "-c", command)
	out, err := cmd.Output()
	if err != nil {
		log.Printf("Error on exec command: %s: %v", command, err)
	}

	return string(out)
}
