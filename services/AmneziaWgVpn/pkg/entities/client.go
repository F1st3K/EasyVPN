package entities

type Client struct {
	Id         string
	PublicKey  string
	PrivateKey string
	Address    string
	IsEnabled  bool
}
