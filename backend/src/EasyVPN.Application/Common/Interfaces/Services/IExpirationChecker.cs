namespace EasyVPN.Application.Common.Interfaces.Services;

public interface IExpirationChecker
{
    public void NewExpire(DateTime expireTime, Func<bool> onExpire);
}