using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}