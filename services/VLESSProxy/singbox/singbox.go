package singbox

import (
	"encoding/json"
	"errors"
	"fmt"
	"log"
	"os"
	"os/exec"
	"strings"
	"syscall"
)

type RealityKeys struct {
	Private string `json:"private_key"`
	Public  string `json:"public_key"`
}

// берем из файла с конфигом ключи, или создаем их при первом запуске.
func GetRealityKeys(cfgpath string) RealityKeys {
	b, err := os.ReadFile(cfgpath)
	if err != nil {
		b, err = GenerateRealityKeys()
		if err != nil {
			panic(err)
		}
		keys, err := ParseRealityKeys(b)
		if err != nil {
			panic(err)
		}
		if err := WriteRealityKeys(keys, cfgpath); err != nil {
			fmt.Println(keys)
			panic(err)
		}
		return keys
	}
	var keys RealityKeys
	err = json.Unmarshal(b, &keys)
	if err != nil {
		panic(err)
	}
	return keys
}

// starting service
func RunSingBox(cfgpath string) (*exec.Cmd, error) {
	cmd := exec.Command(
		"sing-box",
		"run",
		"-c",
		cfgpath,
	)
	log.Println("Запущено!")
	cmd.Stdout = os.Stdout
	cmd.Stderr = os.Stderr
	if err := cmd.Start(); err != nil {
		return nil, err
	}

	return cmd, nil
}

// reading new config file
func ReloadSingBox(cmd *exec.Cmd) error {
	return cmd.Process.Signal(syscall.SIGHUP)
}

// generating reality keys (for first start of service)
func GenerateRealityKeys() ([]byte, error) {
	cmd := exec.Command("sing-box", "generate", "reality-keypair")
	output, err := cmd.Output()
	if err != nil {
		return nil, err
	}
	return output, nil
}

func CheckCfg(path string) error {
	cmd := exec.Command("sing-box", "check", "-c", path)
	output, err := cmd.CombinedOutput()
	log.Println(output)
	return err
}

// utility for parse values
func ParseRealityKeys(b []byte) (RealityKeys, error) {
	var keys RealityKeys

	lines := strings.Split(string(b), "\n")

	for _, line := range lines {
		line = strings.TrimSpace(line)

		private, found := strings.CutPrefix(line, "PrivateKey:")
		if found {
			keys.Private = strings.TrimSpace(private)
		}

		public, found := strings.CutPrefix(line, "PublicKey:")
		if found {
			keys.Public = strings.TrimSpace(public)
		}
	}

	if keys.Private == "" || keys.Public == "" {
		return RealityKeys{}, errors.New("failed to parse reality keys")
	}

	return keys, nil
}

// save pair of keys to json
func WriteRealityKeys(keys RealityKeys, filepath string) error {
	b, err := json.MarshalIndent(keys, "", "  ")
	if err != nil {
		return err
	}

	if err := os.WriteFile(filepath, b, 0600); err != nil {
		return err
	}
	return nil
}
