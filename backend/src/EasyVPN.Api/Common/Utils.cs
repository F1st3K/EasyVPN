using System.Security.Claims;

namespace EasyVPN.Api.Common;

public static class Utils
{
    public static Guid? GetCurrentId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is not null && Guid.TryParse(id, out var guid))
            return guid;

        return null;
    }

    public static string? GetCurrentToken(this HttpRequest request)
    {
        var token = request.Headers.Authorization;
        return token.ToString().Split(' ').Last();
    }
}