using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}