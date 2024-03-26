package wireguardvpn

import (
	"github.com/gin-gonic/gin"
)

type Auth struct {
	Username string
	Password string
}

func (auth *Auth) CheckBasicAuth(c *gin.Context) {
	// Get the Basic Authentication credentials
	user, password, hasAuth := c.Request.BasicAuth()
	if !hasAuth || user != auth.Username || password != auth.Password {
		c.AbortWithStatus(401) //Unauthorized
	}
}
