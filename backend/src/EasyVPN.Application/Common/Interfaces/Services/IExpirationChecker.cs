using ErrorOr;

namespace EasyVPN.Application.Common.Interfaces.Services;

public interface IExpirationChecker
{
    public void Run();
    public void NewExpire(DateTime expireTime, Func<ErrorOr<Success>> onExpire);
}