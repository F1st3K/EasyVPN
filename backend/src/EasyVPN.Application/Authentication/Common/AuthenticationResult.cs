using EasyZsV.Domain.Entities;
using EasyZsV.Domain.Common.Enums;

namespace EasyZsV.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
    );