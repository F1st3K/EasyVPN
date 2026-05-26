package singbox

import (
	"encoding/json"
	"os"
)

type SingBoxConfig struct {
	Log       Log        `json:"log"`
	Inbounds  []Inbound  `json:"inbounds"`
	Outbounds []Outbound `json:"outbounds"`
	Route     Route      `json:"route"`
}

type Log struct {
	Level string `json:"level"`
}

type Inbound struct {
	Type       string `json:"type"`
	Tag        string `json:"tag"`
	Listen     string `json:"listen"`
	ListenPort int    `json:"listen_port"`
	Users      []User `json:"users"`

	TLS *TLS `json:"tls,omitempty"`
}

type User struct {
	UUID string `json:"uuid"`
	Flow string `json:"flow,omitempty"`
}

type TLS struct {
	Enabled    bool     `json:"enabled"`
	ServerName string   `json:"server_name"`
	Reality    *Reality `json:"reality,omitempty"`
}

type Reality struct {
	Enabled    bool      `json:"enabled"`
	Handshake  Handshake `json:"handshake"`
	PrivateKey string    `json:"private_key"`
	ShortID    []string  `json:"short_id"`
}

type Handshake struct {
	Server     string `json:"server"`
	ServerPort int    `json:"server_port"`
}

type Outbound struct {
	Type string `json:"type"`
	Tag  string `json:"tag"`
}

type Route struct {
	Rules []Rule `json:"rules"`
}

type Rule struct {
	Outbound string   `json:"outbound"`
	Protocol []string `json:"protocol"`
}

func NewSingBoxConfig(pri, sid, server string, listenPort int, usersUUID []string) ([]byte, error) {
	var users []User

	for _, s := range usersUUID {
		users = append(users, User{UUID: s, Flow: "xtls-rprx-vision"})
	}

	config := SingBoxConfig{
		Log: Log{Level: "info"},
		Inbounds: []Inbound{
			{
				Type:       "vless",
				Tag:        "vless-reality",
				Listen:     "::",
				ListenPort: listenPort,
				Users:      users,
				TLS: &TLS{
					Enabled:    true,
					ServerName: server,
					Reality: &Reality{
						Enabled: true,
						Handshake: Handshake{
							Server:     server,
							ServerPort: 443,
						},
						PrivateKey: pri,
						ShortID:    []string{sid},
					},
				},
			},
		},
		Outbounds: []Outbound{
			{Type: "direct", Tag: "direct"},
			{Type: "block", Tag: "block"},
		},
		Route: Route{
			Rules: []Rule{
				{Outbound: "block", Protocol: []string{"quic"}},
			},
		},
	}

	b, err := json.MarshalIndent(config, "", "  ")

	if err != nil {
		return nil, err
	}
	return b, nil
}

func WriteConfig(cfg []byte, filepath string) error {
	tmp := filepath + ".tmp"
	if err := os.WriteFile(tmp, cfg, 0644); err != nil {
		return err
	}
	if err := CheckCfg(tmp); err != nil {
		return err
	}

	return os.Rename(tmp, filepath)
}
