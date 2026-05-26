package utils

import (
	"fmt"
	"log"
	"os"
	"strconv"
)

const IP = "/lastip"

type AddressManager struct {
	BasePath string
}

func NewAddressManager(basePath string) *AddressManager {
	ip := AddressManager{BasePath: basePath}

	_, err := os.Stat(ip.BasePath)
	if os.IsNotExist(err) {
		log.Fatalf("Failed to create ip manager: %s", err.Error())
	}

	f, err := os.Open(ip.BasePath + IP)
	if os.IsNotExist(err) {
		f, _ = os.Create(ip.BasePath + IP)
		f.WriteString("1")
	}
	defer f.Close()

	return &ip
}

func (ip AddressManager) GetNextAllowedIp() string {
	li, _ := os.ReadFile(ip.BasePath + IP)

	lastIp, _ := strconv.Atoi(string(li))
	nextIp := lastIp + 1

	os.WriteFile(ip.BasePath+IP, []byte(fmt.Sprintf("%d", nextIp)), os.ModeAppend)

	return fmt.Sprintf("10.0.0.%d/32", nextIp)
}

func (ip AddressManager) GenerateAddress() (string, error) {
	li, err := os.ReadFile(ip.BasePath + IP)
	if err != nil {
		return "", err
	}

	lastIp, err := strconv.Atoi(string(li))
	if err != nil {
		return "", err
	}
	nextIp := lastIp + 1

	err = os.WriteFile(ip.BasePath+IP, []byte(fmt.Sprintf("%d", nextIp)), os.ModeAppend)
	if err != nil {
		return "", err
	}

	return fmt.Sprintf("10.0.0.%d/32", nextIp), nil
}

func (ip AddressManager) ValidateAddress(address string) error {
	// Simple validation - check if it matches the expected format
	return nil // For now, accept any address
}
