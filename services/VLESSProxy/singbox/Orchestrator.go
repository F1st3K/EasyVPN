package singbox

import (
	"VlessProxy/config"
	"fmt"
	"os/exec"
	"sync"
)

type Orchestrator struct {
	mu         sync.Mutex
	sbconfig   Config
	MainConfig *config.Config
	Process    *exec.Cmd
	Keys       RealityKeys
}

func NewOrchestrator(MainConfig *config.Config) *Orchestrator {
	keys := GetRealityKeys(MainConfig.KeyPath)
	process, err := RunSingBox(MainConfig.SingBoxCfgPath)
	if err != nil {
		panic(err)
	}
	return &Orchestrator{
		sbconfig:   NewConfig(MainConfig.ShortID, MainConfig.ServerName, MainConfig.Port),
		mu:         sync.Mutex{},
		MainConfig: MainConfig,
		Process:    process,
		Keys:       keys,
	}
}

func (o *Orchestrator) UpdateConfigAndReload(userUUID []string) error {
	o.mu.Lock()
	defer o.mu.Unlock()
	cfg, err := NewSingBoxConfig(o.Keys.Private, o.sbconfig.ShortID, o.sbconfig.ServerName, o.MainConfig.SingBoxPort, userUUID)
	if err != nil {
		return err
	}
	if err := WriteConfig(cfg, o.MainConfig.SingBoxCfgPath); err != nil {
		return err
	}

	return ReloadSingBox(o.Process)
}

func (o *Orchestrator) CreateLink(userUUID string) string {
	return fmt.Sprintf(
		"vless://%s@%s:%s?security=reality&sid=%s&sni=%s&fp=chrome&pbk=%s&type=tcp&flow=xtls-rprx-vision",
		userUUID,
		o.MainConfig.ServerDomain,
		o.MainConfig.SingBoxPort,
		o.sbconfig.ShortID,
		o.sbconfig.ServerName,
		o.Keys.Public,
	)
}
