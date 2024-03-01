using EasyVPN.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Options = EasyVPN.Infrastructure.Settings.Options;
using System.Timers;
using Timer = System.Timers.Timer;

namespace EasyVPN.Infrastructure.Services;

public class ExpirationChecker : IExpirationChecker
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Timer _timer;
    private List<(DateTime expireTime, Func<bool> onExpire)> _expires = new();

    public ExpirationChecker(
        IOptions<Options.Expiration> expirationOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _timer = new Timer();
        _timer.Interval = TimeSpan.FromMinutes(
            expirationOptions.Value.CheckMinutes).Milliseconds;
        _timer.Elapsed += Check;
        _timer.Start();
    }

    public void NewExpire(DateTime expireTime, Func<bool> onExpire)
    {
        _expires.Add((expireTime, onExpire));
    }

    private void Check(object? sender, ElapsedEventArgs e)
    {
        _expires = _expires.AsParallel()
            .Where(expire =>
        {
            if (expire.expireTime > _dateTimeProvider.UtcNow)
                return true;
            return expire.onExpire() == false;
        }).ToList();
    }
}