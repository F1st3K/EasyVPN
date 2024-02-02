using EasyVPN.Application.Common.Interfaces.Services;

namespace EasyVPN.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}