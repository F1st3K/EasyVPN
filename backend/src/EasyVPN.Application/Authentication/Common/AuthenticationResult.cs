using EasyVPN.Domain.Entities;
using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
    );