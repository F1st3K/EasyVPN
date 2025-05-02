namespace EasyVPN.Contracts.Users;

public record ClientResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Icon,
    string Login
    );