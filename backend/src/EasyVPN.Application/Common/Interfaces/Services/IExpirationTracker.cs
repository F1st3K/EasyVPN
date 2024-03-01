namespace EasyVPN.Application.Common.Interfaces.Services;

public interface IExpirationTracker
{
    public void NewExpire(DateTime expireTime, Func<bool> onExpire);
}