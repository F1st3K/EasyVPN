package wireguardvpn

import (
	"os"
	"strings"

	"github.com/gin-gonic/gin"
)

func (h *Handler) GetConnectionConfig(c *gin.Context) {
	id := c.Param("id")

	c.String(200, "configuraiton -> "+id) // OK
	//c.JSON(200, "nicceess "+u+p)
	//fmt.Fprintln(c.Writer, "Nicceee")
}

func (h *Handler) CreateConnection(c *gin.Context) {
	id := c.Query("id")
	client := DIS_CLIENTS + "/" + id

	_ = os.MkdirAll(client, os.ModeAppend)

	tryCreateKeys(client+PRIVATE_KEY, client+PUBLIC_KEY)

	_, err := os.Stat(client + IP)
	if os.IsNotExist(err) {
		f, _ := os.Create(client + IP)
		f.WriteString(getNextAllowedIp())
		f.Close()
	}

	c.Status(201) // Created
}

func (h *Handler) EnableConnection(c *gin.Context) {
	id := c.Param("id")
	disclient := DIS_CLIENTS + "/" + id
	client := CLIENTS + "/" + id

	// Move client files on disabled to clients
	_ = os.MkdirAll(client, os.ModeAppend)

	f, _ := os.Create(client + PRIVATE_KEY)
	private, _ := os.ReadFile(disclient + PRIVATE_KEY)
	f.WriteString(string(private))
	f.Close()
	f, _ = os.Create(client + PUBLIC_KEY)
	public, _ := os.ReadFile(disclient + PUBLIC_KEY)
	f.WriteString(string(public))
	f.Close()
	f, _ = os.Create(client + IP)
	ip, _ := os.ReadFile(disclient + IP)
	f.WriteString(string(ip))
	f.Close()

	os.RemoveAll(disclient)

	//Rebuild wireguard configuration
	wgDown()
	buildServerConfiguration()
	wgUp()

	c.Status(204) // No Content
}

func (h *Handler) DisableConnection(c *gin.Context) {
	id := c.Param("id")
	strings.Split(id, "")

	c.Status(204) // No Content
}

func (h *Handler) DeleteConnection(c *gin.Context) {
	id := c.Param("id")
	strings.Split(id, "")

	c.Status(204) // No Content
}
