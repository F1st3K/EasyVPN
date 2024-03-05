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
    private Dictionary<Guid, (DateTime expireTime, Func<ErrorOr<Success>> onExpire)> _expires = new();

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

    public Guid NewExpire(DateTime expireTime, Func<ErrorOr<Success>> onExpire)
    {
        var expireId = Guid.NewGuid();
        _expires.Add(expireId, (expireTime, onExpire));
        return expireId;
    }

    public bool TryRemoveExpire(Guid expireId) => _expires.Remove(expireId);

    private void Check()
    {
        _expires.AsParallel()
            .ForAll(expire =>
            {
                if (expire.Value.expireTime > _dateTimeProvider.UtcNow) return;
                if (expire.Value.onExpire() == new Success())
                    _expires.Remove(expire.Key);
            });
    }

    private static int ToMilliseconds(int minutes)
        => minutes * 60_000;
}