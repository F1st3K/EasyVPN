package singbox

type Config struct {
	ShortID    string
	ServerName string
	ListenPort int
}

func NewConfig(short, server string, port int) Config {
	return Config{
		ShortID:    short,
		ServerName: server,
		ListenPort: port,
	}
}
