namespace EasyVPN.Contracts.Users;

public record ClientResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Icon,
    string Login
    );