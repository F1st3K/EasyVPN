using EasyZsV.Contracts.Users;

namespace EasyZsV.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Icon,
    string Login,
    string[] Roles,
    string Token
    );