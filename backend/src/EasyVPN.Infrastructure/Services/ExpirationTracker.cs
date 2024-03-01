using EasyVPN.Application.Common.Interfaces.Services;

namespace EasyVPN.Infrastructure.Services;

public class ExpirationTracker : IExpirationTracker
{
    private readonly List<(DateTime expireTime, Func<bool> onExpire)> _expires = new();

    public ExpirationTracker()
    {
    }
    
    public void NewExpire(DateTime expireTime, Func<bool> onExpire)
    {
        _expires.Add((expireTime, onExpire));
    }
}