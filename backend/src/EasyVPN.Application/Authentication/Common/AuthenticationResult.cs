using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
    );