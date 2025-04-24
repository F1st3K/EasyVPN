namespace EasyVPN.Contracts.Users;

public record UserRequest(
    string FirstName,
    string LastName,
    string Icon
    );