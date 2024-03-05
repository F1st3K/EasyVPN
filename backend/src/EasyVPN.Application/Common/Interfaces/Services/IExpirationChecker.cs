using ErrorOr;

namespace EasyVPN.Application.Common.Interfaces.Services;

public interface IExpirationChecker
{
    public void Run();
    public Guid NewExpire(DateTime expireTime, Func<ErrorOr<Success>> onExpire);
    public bool TryRemoveExpire(Guid expireId);
}