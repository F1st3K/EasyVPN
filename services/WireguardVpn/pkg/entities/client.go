package entities

type Client struct {
	Id         string
	PublicKey  string
	PrivateKey string
	AllowedIPs string
	IsEnabled  bool
}
