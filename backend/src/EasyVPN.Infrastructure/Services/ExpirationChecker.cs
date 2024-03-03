using EasyVPN.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Options = EasyVPN.Infrastructure.Settings.Options;
using ErrorOr;
using Timer = System.Timers.Timer;

namespace EasyVPN.Infrastructure.Services;

public class ExpirationChecker : IExpirationChecker
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Timer _timer;
    private List<(DateTime expireTime, Func<ErrorOr<Success>> onExpire)> _expires = new();

    public ExpirationChecker(
        IOptions<Options.Expiration> expirationOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _timer = new Timer();
        _timer.Interval = ToMilliseconds(expirationOptions.Value.CheckMinutes);
        _timer.Elapsed += (sender, e) => Check();
    }

    public void Run()
    {
        Check();
        _timer.Start();
    }

    public void NewExpire(DateTime expireTime, Func<ErrorOr<Success>> onExpire)
    {
        _expires.Add((expireTime, onExpire));
    }

    private void Check()
    {
        _expires = _expires.AsParallel()
            .Where(expire =>
        {
            if (expire.expireTime > _dateTimeProvider.UtcNow)
                return true;
            return expire.onExpire() != new Success();
        }).ToList();
    }

    private static int ToMilliseconds(int minutes)
        => minutes * 60_000;
}