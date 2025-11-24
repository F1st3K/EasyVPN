using EasyZsV.Application.Common.Interfaces.Services;

namespace EasyZsV.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}