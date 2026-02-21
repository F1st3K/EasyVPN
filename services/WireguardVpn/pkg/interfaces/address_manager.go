package interfaces

// AddressManager interface defines the contract for address management operations
type AddressManager interface {
	GenerateAddress() (string, error)
	ValidateAddress(address string) error
}
