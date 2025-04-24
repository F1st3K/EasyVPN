namespace EasyVPN.Contracts.Users;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Icon,
    string Login,
    string[] Roles
    );