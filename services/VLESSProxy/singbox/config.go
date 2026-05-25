package singbox

type Config struct {
	ShortID    string
	ServerName string
	ListenPort string
}

func NewConfig(short, server, port string) Config {
	return Config{
		ShortID:    short,
		ServerName: server,
		ListenPort: port,
	}
}
