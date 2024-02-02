using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
    );