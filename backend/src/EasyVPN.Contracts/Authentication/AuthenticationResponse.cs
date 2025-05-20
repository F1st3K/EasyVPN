using EasyVPN.Contracts.Users;

namespace EasyVPN.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Icon,
    string Login,
    string[] Roles,
    string Token
    );