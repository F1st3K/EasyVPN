using EasyVPN.Contracts.Users;

namespace EasyVPN.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Login,
    string[] Roles,
    string Token
    ) : UserResponse(Id, FirstName, LastName, Login, Roles);